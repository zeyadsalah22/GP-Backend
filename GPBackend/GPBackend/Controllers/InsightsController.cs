using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using GPBackend.DTOs;
using GPBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InsightsController : ControllerBase
    {
        private readonly IInsightsService _insightsService;

        public InsightsController(IInsightsService insightsService)
        {
            _insightsService = insightsService;
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<StatisticsDTO>> GetStatistics()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
                {
                    return Unauthorized();
                }
                var statistics = await _insightsService.GetStatisticsAsync(id);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("timeseries")]
        public async Task<ActionResult<TimeSeriesDTO>> GetTimeSeries(
            [FromQuery] string start_date = null,
            [FromQuery] int? points = null,
            [FromQuery] string interval = "week")
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
                {
                    return Unauthorized();
                }
                DateTime? parsedDate = null;

                if (!string.IsNullOrEmpty(start_date))
                {
                    try
                    {
                        parsedDate = DateTime.ParseExact(start_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        return BadRequest(new { error = new[] { "Invalid date format. Use: YYYY-MM-DD" } });
                    }
                }

                var timeSeries = await _insightsService.GetTimeSeriesAsync(id, parsedDate, points, interval);
                return Ok(timeSeries);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = new[] { ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("percents")]
        public async Task<ActionResult<PercentsDTO>> GetPercents()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
                {
                    return Unauthorized();
                }
                var percents = await _insightsService.GetPercentsAsync(id);
                return Ok(percents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
} 