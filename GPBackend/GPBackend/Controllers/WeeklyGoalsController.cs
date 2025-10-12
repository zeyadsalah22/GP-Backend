using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GPBackend.DTOs.WeeklyGoal;
using GPBackend.DTOs.Common;
using GPBackend.Services.Interfaces;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.Controllers
{
    [Authorize]
    [Route("api/weekly-goals")]
    [ApiController]
    public class WeeklyGoalsController : ControllerBase
    {
        private readonly IWeeklyGoalService _weeklyGoalService;

        public WeeklyGoalsController(IWeeklyGoalService weeklyGoalService)
        {
            _weeklyGoalService = weeklyGoalService;
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

        // GET: api/weekly-goals
        [HttpGet]
        public async Task<ActionResult<PagedResult<WeeklyGoalResponseDto>>> GetWeeklyGoals([FromQuery] WeeklyGoalQueryDto queryDto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _weeklyGoalService.GetFilteredWeeklyGoalsAsync(userId, queryDto);
                
                Response.Headers.Add("X-Pagination-TotalCount", result.TotalCount.ToString());
                Response.Headers.Add("X-Pagination-PageSize", result.PageSize.ToString());
                Response.Headers.Add("X-Pagination-CurrentPage", result.PageNumber.ToString());
                Response.Headers.Add("X-Pagination-TotalPages", result.TotalPages.ToString());
                Response.Headers.Add("X-Pagination-HasNext", result.HasNext.ToString());
                Response.Headers.Add("X-Pagination-HasPrevious", result.HasPrevious.ToString());

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // GET: api/weekly-goals/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WeeklyGoalResponseDto>> GetWeeklyGoalById(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var weeklyGoal = await _weeklyGoalService.GetWeeklyGoalByIdAsync(id, userId);
                
                if (weeklyGoal == null)
                {
                    return NotFound();
                }
                
                return Ok(weeklyGoal);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // GET: api/weekly-goals/current
        [HttpGet("current")]
        public async Task<ActionResult<WeeklyGoalResponseDto>> GetCurrentWeekGoal()
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var weeklyGoal = await _weeklyGoalService.GetCurrentWeekGoalAsync(userId);
                
                if (weeklyGoal == null)
                {
                    return NotFound(new { message = "No goal set for the current week" });
                }
                
                return Ok(weeklyGoal);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // GET: api/weekly-goals/stats
        [HttpGet("stats")]
        public async Task<ActionResult<WeeklyGoalStatsDto>> GetWeeklyGoalStats()
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var stats = await _weeklyGoalService.GetWeeklyGoalStatsAsync(userId);
                return Ok(stats);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // POST: api/weekly-goals
        [HttpPost]
        public async Task<ActionResult<WeeklyGoalResponseDto>> CreateWeeklyGoal([FromBody][Required] WeeklyGoalCreateDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    return BadRequest(new { message = "Request body is required" });
                }
                
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }
                
                int userId = GetAuthenticatedUserId();
                
                try
                {
                    var createdGoal = await _weeklyGoalService.CreateWeeklyGoalAsync(userId, createDto);
                    return CreatedAtAction(
                        nameof(GetWeeklyGoalById),
                        new { id = createdGoal.WeeklyGoalId },
                        createdGoal
                    );
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // PUT: api/weekly-goals/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeeklyGoal(int id, [FromBody][Required] WeeklyGoalUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null)
                {
                    return BadRequest(new { message = "Request body is required" });
                }
                
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }
                
                int userId = GetAuthenticatedUserId();
                var result = await _weeklyGoalService.UpdateWeeklyGoalAsync(id, userId, updateDto);
                
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

        // DELETE: api/weekly-goals/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeeklyGoal(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _weeklyGoalService.DeleteWeeklyGoalAsync(id, userId);
                
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

