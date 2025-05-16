using GPBackend.DTOs.Resume;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace GPBackend.Controllers
{
    [Route("api/cvs")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        // GET: api/cvs?userId={userId}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResumeResponseDto>>> GetAllResumes(int userId)
        {
            var result = await _resumeService.GetAllResumesAsync(userId);

            return Ok(result);
        }

        // GET: api/cvs/{id}?userId={userId}
        [HttpGet("{id}")]
        public async Task<ActionResult<ResumeResponseDto>> GetResumeById(int id, [FromQuery] int userId)
        {
            var resume = await _resumeService.GetResumeByIdAsync(id);
            if (resume == null)
            {
                return NotFound();
            }
            
            // Verify the resume belongs to the specified user
            if (resume.UserId != userId)
            {
                return Forbid();
            }
            
            return Ok(resume);
        }

        // POST: api/cvs
        [HttpPost]
        public async Task<ActionResult<ResumeResponseDto>> CreateResume([FromForm] int userId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded");
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            
            var resumeDto = new ResumeCreateDto
            {
                UserId = userId,
                ResumeFile = memoryStream.ToArray()
            };
            
            var createdResume = await _resumeService.CreateResumeAsync(resumeDto);
            return CreatedAtAction(nameof(GetResumeById), new { id = createdResume.ResumeId, userId = createdResume.UserId }, createdResume);
        }

        // PUT: api/cvs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResume(int id, [FromForm] int userId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded");
            }
            
            var existingResume = await _resumeService.GetResumeByIdAsync(id);
            if (existingResume == null)
            {
                return NotFound();
            }
            
            if (existingResume.UserId != userId)
            {
                return Forbid();
            }
            
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            
            var resumeDto = new ResumeUpdateDto
            {
                UserId = userId,
                ResumeFile = memoryStream.ToArray()
            };
            
            var result = await _resumeService.UpdateResumeAsync(id, resumeDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/cvs/{id}?userId={userId}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResume(int id, [FromQuery] int userId)
        {
            // Verify the resume belongs to the specified user
            var existingResume = await _resumeService.GetResumeByIdAsync(id);
            if (existingResume == null)
            {
                return NotFound();
            }
            
            if (existingResume.UserId != userId)
            {
                return Forbid();
            }
            
            var result = await _resumeService.DeleteResumeAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
