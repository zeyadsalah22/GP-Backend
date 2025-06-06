using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.ResumeMatching;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GPBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeMatchingController : ControllerBase
    {
        private readonly IResumeMatchingService _resumeMatchingService;

        public ResumeMatchingController(IResumeMatchingService resumeMatchingService)
        {
            _resumeMatchingService = resumeMatchingService;
        }

        /// <summary>
        /// Match a resume against a job description using AI
        /// </summary>
        /// <param name="request">Contains resume ID and job description</param>
        /// <returns>Matching score and extracted skills</returns>
        [HttpPost("match")]
        public async Task<ActionResult<ResumeMatchingResponseDto>> MatchResume([FromBody] ResumeMatchingRequestDto request)
        {
            try
            {
                var result = await _resumeMatchingService.MatchResumeAsync(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the error here
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
} 