using GPBackend.DTOs.Common;
using GPBackend.DTOs.Industry;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPBackend.Controllers
{
	[ApiController]
	[Route("api/industries")]
	public class IndustriesController : ControllerBase
	{
		private readonly IIndustryService _industryService;

		public IndustriesController(IIndustryService industryService)
		{
			_industryService = industryService;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult<PagedResult<IndustryResponseDto>>> GetAll([FromQuery] IndustryQueryDto queryDto)
		{
			var result = await _industryService.GetFilteredAsync(queryDto);
			Response.Headers.Append("X-Pagination-TotalCount", result.TotalCount.ToString());
			Response.Headers.Append("X-Pagination-PageSize", result.PageSize.ToString());
			Response.Headers.Append("X-Pagination-CurrentPage", result.PageNumber.ToString());
			Response.Headers.Append("X-Pagination-TotalPages", result.TotalPages.ToString());
			Response.Headers.Append("X-Pagination-HasNext", result.HasNext.ToString());
			Response.Headers.Append("X-Pagination-HasPrevious", result.HasPrevious.ToString());
			return Ok(result);
		}

		[HttpGet("all")]
		[AllowAnonymous]
		public async Task<ActionResult<IEnumerable<IndustryResponseDto>>> GetAllSimple()
		{
			var result = await _industryService.GetAllAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		[AllowAnonymous]
		public async Task<ActionResult<IndustryResponseDto>> GetById(int id)
		{
			var item = await _industryService.GetByIdAsync(id);
			if (item == null) return NotFound();
			return Ok(item);
		}
	}
}


