using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.InterviewFeedback;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.Controllers
{
    [Route("api/interview")]
    [ApiController]
    [Authorize]
    public class MLInterviewController : ControllerBase
    {
        private readonly IMLServiceClient _mlServiceClient;
        private readonly IInterviewFeedbackService _interviewFeedbackService;
        private readonly IResumeService _resumeService;
        private readonly ILogger<MLInterviewController> _logger;

        public MLInterviewController(
            IMLServiceClient mlServiceClient,
            IInterviewFeedbackService interviewFeedbackService,
            IResumeService resumeService,
            ILogger<MLInterviewController> logger)
        {
            _mlServiceClient = mlServiceClient;
            _interviewFeedbackService = interviewFeedbackService;
            _resumeService = resumeService;
            _logger = logger;
        }

        private int GetAuthenticatedUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated properly");
            }
            return userId;
        }

        // Maximum file size: 10MB
        private const long MaxFileSize = 10 * 1024 * 1024;

        // Maximum video size: 100MB
        private const long MaxVideoSize = 100 * 1024 * 1024;

        // Helper method to validate PDF files
        private async Task<(bool isValid, string errorMessage)> ValidatePdfFileAsync(IFormFile file)
        {
            // Check if file exists
            if (file == null || file.Length == 0)
            {
                return (false, "No file was uploaded");
            }

            // Check file size
            if (file.Length > MaxFileSize)
            {
                return (false, $"File size exceeds maximum allowed size of {MaxFileSize / (1024 * 1024)}MB");
            }

            // Check file extension
            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            if (extension != ".pdf")
            {
                return (false, "Only PDF files are allowed");
            }

            // Check declared Content-Type
            if (file.ContentType != "application/pdf")
            {
                return (false, "Invalid content type. Must be application/pdf");
            }

            // Check actual file content for PDF magic bytes
            using (var stream = file.OpenReadStream())
            {
                // Check file header (%PDF)
                byte[] header = new byte[5];
                int bytesRead = await stream.ReadAsync(header, 0, 5);

                if (bytesRead < 5 ||
                    header[0] != 0x25 ||  // '%'
                    header[1] != 0x50 ||  // 'P'
                    header[2] != 0x44 ||  // 'D'
                    header[3] != 0x46 ||  // 'F'
                    header[4] != 0x2D)    // '-'
                {
                    return (false, "File is not a valid PDF (missing %PDF- header)");
                }

                // Check for PDF EOF marker (%%EOF)
                if (stream.Length > 1024)
                {
                    stream.Seek(-1024, SeekOrigin.End);
                    byte[] endBytes = new byte[1024];
                    await stream.ReadAsync(endBytes, 0, 1024);

                    string endContent = System.Text.Encoding.ASCII.GetString(endBytes);
                    if (!endContent.Contains("%%EOF"))
                    {
                        return (false, "File is not a valid PDF (missing %%EOF marker)");
                    }
                }
            }

            return (true, string.Empty);
        }

        private (bool isValid, string errorMessage) ValidateVideoFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return (false, "No video file was uploaded");
            }

            if (file.Length > MaxVideoSize)
            {
                return (false, $"Video size exceeds maximum allowed size of {MaxVideoSize / (1024 * 1024)}MB");
            }

            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            var allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                ".mp4", ".avi", ".mov", ".mkv", ".webm"
            };

            if (string.IsNullOrWhiteSpace(extension) || !allowed.Contains(extension))
            {
                return (false, "Invalid video format. Allowed formats: mp4, avi, mov, mkv, webm");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Generate interview questions based on job description
        /// </summary>
        [HttpPost("generate-questions")]
        public async Task<ActionResult<GenerateQuestionsResponseDto>> GenerateQuestionsAsync([FromBody] GenerateQuestionsRequestDto request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Description))
                {
                    return BadRequest(new { message = "Description is required." });
                }

                int numQuestions = request.NumQuestions ?? 3;
                if (numQuestions < 1 || numQuestions > 20)
                {
                    return BadRequest(new { message = "NumQuestions must be between 1 and 20." });
                }

                _logger.LogInformation("Generating {NumQuestions} questions for job description (length: {Length})",
                    numQuestions, request.Description.Length);

                var questions = await _mlServiceClient.GenerateQuestionsAsync(request.Description, numQuestions);

                return Ok(new GenerateQuestionsResponseDto
                {
                    Questions = questions
                });
            }
            catch (TimeoutException ex)
            {
                _logger.LogError(ex, "Timeout while generating questions");
                return StatusCode(504, new { message = "Request timed out. The ML service is taking too long to respond.", details = ex.Message });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error while generating questions");
                return StatusCode(502, new { message = "Error communicating with ML service.", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating questions");
                return StatusCode(500, new { message = "An error occurred while generating questions.", details = ex.Message });
            }
        }

        /// <summary>
        /// Match a resume file against a job description
        /// </summary>
        [HttpPost("match-resume")]
        public async Task<ActionResult<ResumeMatchingResponse>> MatchResumeAsync([FromForm] MatchResumeFileRequestDto request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { message = "Request is required." });
                }

                if (request.ResumeFile == null || request.ResumeFile.Length == 0)
                {
                    return BadRequest(new { message = "Resume file is required." });
                }

                // Validate PDF file
                var (isValid, errorMessage) = await ValidatePdfFileAsync(request.ResumeFile);
                if (!isValid)
                {
                    return BadRequest(new { message = errorMessage });
                }

                if (string.IsNullOrWhiteSpace(request.JobDescription))
                {
                    return BadRequest(new { message = "Job description is required." });
                }

                // Convert file to base64
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await request.ResumeFile.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                string base64Resume = Convert.ToBase64String(fileBytes);

                _logger.LogInformation("Matching resume file (size: {Size} bytes) against job description (length: {Length})",
                    fileBytes.Length, request.JobDescription.Length);

                var matchingResult = await _mlServiceClient.MatchResumeAsync(base64Resume, request.JobDescription);

                return Ok(matchingResult);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError(ex, "Timeout while matching resume");
                return StatusCode(504, new { message = "Request timed out. The ML service is taking too long to respond.", details = ex.Message });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error while matching resume");
                return StatusCode(502, new { message = "Error communicating with ML service.", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error matching resume");
                return StatusCode(500, new { message = "An error occurred while matching resume.", details = ex.Message });
            }
        }

        /// <summary>
        /// Health check endpoint to verify ML service is reachable
        /// </summary>
        [HttpGet("health")]
        [AllowAnonymous]
        public async Task<ActionResult<HealthCheckResponseDto>> HealthCheckAsync()
        {
            try
            {
                var isHealthy = await _mlServiceClient.HealthCheckAsync();

                return Ok(new HealthCheckResponseDto
                {
                    IsHealthy = isHealthy,
                    Message = isHealthy ? "ML service is reachable" : "ML service is not reachable",
                    Timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking ML service health");
                return Ok(new HealthCheckResponseDto
                {
                    IsHealthy = false,
                    Message = $"Error checking health: {ex.Message}",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        // ===== Interview-bound feedback APIs (persisted) =====

        [HttpPost("{interviewId:int}/feedback/grade-question")]
        public async Task<ActionResult<InterviewQuestionFeedbackResponseDto>> GradeInterviewQuestionAsync(
            int interviewId,
            [FromBody] GradeInterviewQuestionRequestDto request)
        {
            try
            {
                int userId = GetAuthenticatedUserId();

                if (request == null || request.InterviewQuestionId <= 0)
                {
                    return BadRequest(new { message = "interviewQuestionId is required." });
                }

                var result = await _interviewFeedbackService.GradeInterviewQuestionAsync(
                    userId,
                    interviewId,
                    request.InterviewQuestionId,
                    request.Context,
                    HttpContext.RequestAborted);

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (TimeoutException ex)
            {
                _logger.LogError(ex, "Timeout while grading interview question");
                return StatusCode(504, new { message = "Request timed out. The Interview Feedback service is taking too long to respond.", details = ex.Message });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error while grading interview question");
                return StatusCode(502, new { message = "Error communicating with Interview Feedback service.", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error grading interview question");
                return StatusCode(500, new { message = "An error occurred while grading the interview question.", details = ex.Message });
            }
        }

        [HttpPost("{interviewId:int}/feedback/grade-questions-batch")]
        public async Task<ActionResult<List<InterviewQuestionFeedbackResponseDto>>> GradeInterviewQuestionsBatchAsync(
            int interviewId,
            [FromBody] GradeInterviewQuestionsBatchRequestDto request)
        {
            try
            {
                int userId = GetAuthenticatedUserId();

                if (request == null || request.InterviewQuestionIds == null || request.InterviewQuestionIds.Count == 0)
                {
                    return BadRequest(new { message = "interviewQuestionIds list is required." });
                }

                var result = await _interviewFeedbackService.GradeInterviewQuestionsBatchAsync(
                    userId,
                    interviewId,
                    request.InterviewQuestionIds,
                    request.Context,
                    HttpContext.RequestAborted);

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (TimeoutException ex)
            {
                _logger.LogError(ex, "Timeout while grading interview questions batch");
                return StatusCode(504, new { message = "Request timed out. The Interview Feedback service is taking too long to respond.", details = ex.Message });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error while grading interview questions batch");
                return StatusCode(502, new { message = "Error communicating with Interview Feedback service.", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error grading interview questions batch");
                return StatusCode(500, new { message = "An error occurred while grading the interview questions batch.", details = ex.Message });
            }
        }

        [HttpGet("{interviewId:int}/feedback/answers")]
        public async Task<ActionResult<List<InterviewQuestionFeedbackResponseDto>>> GetAnswersFeedbackAsync(int interviewId)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _interviewFeedbackService.GetAnswersFeedbackAsync(userId, interviewId, HttpContext.RequestAborted);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("{interviewId:int}/feedback/analyze-video")]
        [RequestSizeLimit(110 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 110 * 1024 * 1024)]
        public async Task<ActionResult<InterviewVideoFeedbackResponseDto>> AnalyzeInterviewVideoAsync(
            int interviewId,
            [FromForm] AnalyzeVideoRequestDto request)
        {
            try
            {
                int userId = GetAuthenticatedUserId();

                if (request == null || request.File == null)
                {
                    return BadRequest(new { message = "Video file is required." });
                }

                var (isValid, errorMessage) = ValidateVideoFile(request.File);
                if (!isValid)
                {
                    return BadRequest(new { message = errorMessage });
                }

                await using var stream = request.File.OpenReadStream();
                var result = await _interviewFeedbackService.AnalyzeInterviewVideoAsync(
                    userId,
                    interviewId,
                    stream,
                    request.File.FileName,
                    request.File.ContentType,
                    HttpContext.RequestAborted);

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (TimeoutException ex)
            {
                _logger.LogError(ex, "Timeout while analyzing interview video");
                return StatusCode(504, new { message = "Request timed out. The Interview Feedback service is taking too long to respond.", details = ex.Message });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error while analyzing interview video");
                return StatusCode(502, new { message = "Error communicating with Interview Feedback service.", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error analyzing interview video");
                return StatusCode(500, new { message = "An error occurred while analyzing the interview video.", details = ex.Message });
            }
        }

        [HttpGet("{interviewId:int}/feedback/video")]
        public async Task<ActionResult<InterviewVideoFeedbackResponseDto>> GetVideoFeedbackAsync(int interviewId)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _interviewFeedbackService.GetVideoFeedbackAsync(userId, interviewId, HttpContext.RequestAborted);
                if (result == null) return NotFound(new { message = "Video feedback not found." });
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // Alternative endpoint: Match resume by ID (uses existing resume in database)
        [HttpPost("match-resume/{resumeId}")]
        public async Task<ActionResult<ResumeMatchingResponse>> MatchResumeByIdAsync(int resumeId, [FromBody] MatchResumeByIdRequestDto request)
        {
            try
            {
                int userId = GetAuthenticatedUserId();

                if (request == null || string.IsNullOrWhiteSpace(request.JobDescription))
                {
                    return BadRequest(new { message = "Job description is required." });
                }

                _logger.LogInformation("Matching resume {ResumeId} for user {UserId} against job description (length: {Length})",
                    resumeId, userId, request.JobDescription.Length);

                var matchingResult = await _resumeService.MatchResumeAsync(resumeId, request.JobDescription, userId);

                return Ok(matchingResult);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt for resume matching");
                return Unauthorized(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid argument for resume matching");
                return BadRequest(new { message = ex.Message });
            }
            catch (TimeoutException ex)
            {
                _logger.LogError(ex, "Timeout while matching resume");
                return StatusCode(504, new { message = "Request timed out. The ML service is taking too long to respond.", details = ex.Message });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error while matching resume");
                return StatusCode(502, new { message = "Error communicating with ML service.", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error matching resume");
                return StatusCode(500, new { message = "An error occurred while matching resume.", details = ex.Message });
            }
        }
    }

    // DTOs for the ML Interview endpoints
    public class GenerateQuestionsRequestDto
    {
        [Required]
        public string Description { get; set; } = string.Empty;
        public int? NumQuestions { get; set; }
    }

    public class GenerateQuestionsResponseDto
    {
        public List<string> Questions { get; set; } = new();
    }

    public class MatchResumeFileRequestDto
    {
        [Required]
        public IFormFile ResumeFile { get; set; } = null!;
        [Required]
        public string JobDescription { get; set; } = string.Empty;
    }

    public class MatchResumeByIdRequestDto
    {
        [Required]
        public string JobDescription { get; set; } = string.Empty;
    }

    public class HealthCheckResponseDto
    {
        public bool IsHealthy { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}

