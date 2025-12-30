using GPBackend.DTOs.NodeRAG;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [Authorize]
    [Route("api/ai-assistant")]
    [ApiController]
    public class NodeRAGController : ControllerBase
    {
        private readonly INodeRAGService _nodeRAGService;
        private readonly ILogger<NodeRAGController> _logger;

        public NodeRAGController(INodeRAGService nodeRAGService, ILogger<NodeRAGController> logger)
        {
            _nodeRAGService = nodeRAGService;
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

        /// <summary>
        /// Generate AI-powered answer to a job application question using user's knowledge graph
        /// </summary>
        [HttpPost("generate-answer")]
        public async Task<ActionResult<NodeRAGAnswerResponseDto>> GenerateAnswer([FromBody] NodeRAGAnswerRequestDto request)
        {
            try
            {
                var userId = GetAuthenticatedUserId();
                var result = await _nodeRAGService.GenerateAnswerAsync(
                    userId, 
                    request.Query, 
                    request.JobContext, 
                    request.TopK);
                
                return Ok(result);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(new { message = "Knowledge graph not found. Please upload a resume first.", details = ex.Message });
            }
            catch (TimeoutException ex)
            {
                return StatusCode(504, new { message = "Request timed out. Please try again.", details = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating answer");
                return StatusCode(500, new { message = "An error occurred while generating answer.", details = ex.Message });
            }
        }

        /// <summary>
        /// Get statistics about user's knowledge graph
        /// </summary>
        [HttpGet("graph-stats")]
        public async Task<ActionResult<NodeRAGGraphStatsDto>> GetGraphStats()
        {
            try
            {
                var userId = GetAuthenticatedUserId();
                var result = await _nodeRAGService.GetUserGraphStatsAsync(userId);
                return Ok(result);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(new { message = "Knowledge graph not found. Please upload a resume first." });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting graph stats");
                return StatusCode(500, new { message = "An error occurred while retrieving graph stats.", details = ex.Message });
            }
        }

        /// <summary>
        /// Manually trigger a knowledge graph rebuild for the current user
        /// </summary>
        [HttpPost("trigger-build")]
        public async Task<ActionResult> TriggerGraphBuild()
        {
            try
            {
                var userId = GetAuthenticatedUserId();
                await _nodeRAGService.TriggerGraphBuildAsync(userId);
                return Accepted(new { message = "Graph build has been queued and will process in the background." });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error triggering graph build");
                return StatusCode(500, new { message = "An error occurred while triggering graph build.", details = ex.Message });
            }
        }
    }
}

