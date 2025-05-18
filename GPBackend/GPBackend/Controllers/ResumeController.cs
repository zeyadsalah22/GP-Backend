﻿using GPBackend.DTOs.Resume;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [Authorize]
    [Route("api/cvs")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        // Helper method to get the authenticated user's ID
        private int GetAuthenticatedUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated properly");
            }
            return userId;
        }

        // GET: api/cvs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResumeResponseDto>>> GetAllResumes()
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _resumeService.GetAllResumesAsync(userId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // GET: api/cvs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ResumeResponseDto>> GetResumeById(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                
                var resume = await _resumeService.GetResumeByIdAsync(id, userId);
                if (resume == null)
                {
                    return NotFound();
                }
                
                return Ok(resume);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // POST: api/cvs
        [HttpPost]
        public async Task<ActionResult<ResumeResponseDto>> CreateResume(IFormFile file)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                
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
                return CreatedAtAction(nameof(GetResumeById), new { id = createdResume.ResumeId }, createdResume);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // PUT: api/cvs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResume(int id, IFormFile file)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file was uploaded");
                }
                
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                
                var resumeDto = new ResumeUpdateDto
                {
                    UserId = userId,
                    ResumeFile = memoryStream.ToArray()
                };
                
                var result = await _resumeService.UpdateResumeAsync(id, resumeDto, userId);
                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // DELETE: api/cvs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResume(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                
                var result = await _resumeService.DeleteResumeAsync(id, userId);
                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}
