using GPBackend.DTOs.Skill;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SkillCreateDto createDto)
        {
            var userId = GetUserId();
            var result = await _skillService.CreateSkillAsync(userId, createDto);
            if (result == null) return BadRequest("Test not found or skill creation failed.");
            return Ok(result);
        }

        [HttpPut("{skillId}")]
        public async Task<IActionResult> Update(int skillId, [FromBody] SkillUpdateDto updateDto)
        {
            var userId = GetUserId();
            var result = await _skillService.UpdateSkillAsync(userId, skillId, updateDto);
            if (result == null) return BadRequest("Skill not found or update failed.");
            return Ok(result);
        }

        [HttpDelete("{skillId}")]
        public async Task<IActionResult> Delete(int skillId)
        {
            var userId = GetUserId();
            var deleted = await _skillService.DeleteSkillAsync(userId, skillId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 