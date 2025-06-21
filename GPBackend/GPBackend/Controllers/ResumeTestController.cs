using GPBackend.DTOs.ResumeTest;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ResumeTestController : ControllerBase
    {
        private readonly IResumeTestService _resumeTestService;

        public ResumeTestController(IResumeTestService resumeTestService)
        {
            _resumeTestService = resumeTestService;
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var tests = await _resumeTestService.GetAllResumeTestsByUserIdAsync(userId);
            return Ok(tests);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] ResumeTestQueryDto queryDto)
        {
            var userId = GetUserId();
            var paged = await _resumeTestService.GetFilteredResumeTestsAsync(userId, queryDto);
            return Ok(paged);
        }

        [HttpGet("{testId}")]
        public async Task<IActionResult> GetById(int testId)
        {
            var userId = GetUserId();
            var test = await _resumeTestService.GetResumeTestByIdAsync(userId, testId);
            if (test == null) return NotFound();
            return Ok(test);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResumeTestCreateDto createDto)
        {
            var userId = GetUserId();
            var result = await _resumeTestService.CreateResumeTestAsync(userId, createDto);
            if (result == null) return BadRequest("Resume not found or AI model failed.");
            return Ok(result);
        }

        [HttpDelete("{testId}")]
        public async Task<IActionResult> Delete(int testId)
        {
            var userId = GetUserId();
            var deleted = await _resumeTestService.DeleteResumeTestAsync(userId, testId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}