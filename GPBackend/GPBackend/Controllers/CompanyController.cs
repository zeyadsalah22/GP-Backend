using Microsoft.AspNetCore.Mvc;
using GPBackend.Models;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Company;
using GPBackend.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        [AllowAnonymous] // Allow listing companies for all users
        public async Task<ActionResult<IEnumerable<CompanyResponseDto>>> GetAllCompanies([FromQuery] CompanyQueryDto queryDto)
        {
            var result = await _companyService.GetFilteredCompaniesAsync(queryDto);
                
            // Add pagination headers
            Response.Headers.Add("X-Pagination-TotalCount", result.TotalCount.ToString());
            Response.Headers.Add("X-Pagination-PageSize", result.PageSize.ToString());
            Response.Headers.Add("X-Pagination-CurrentPage", result.PageNumber.ToString());
            Response.Headers.Add("X-Pagination-TotalPages", result.TotalPages.ToString());
            Response.Headers.Add("X-Pagination-HasNext", result.HasNext.ToString());
            Response.Headers.Add("X-Pagination-HasPrevious", result.HasPrevious.ToString());

            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyResponseDto>> GetCompanyById(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<CompanyResponseDto>> CreateCompany([FromBody][Required] CompanyCreateDto companyDto)
        {
            if (companyDto == null)
            {
                return BadRequest(new { message = "Request body is required" });
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var createdCompany = await _companyService.CreateCompanyAsync(companyDto);
            return CreatedAtAction(nameof(GetCompanyById), new { id = createdCompany.CompanyId }, createdCompany);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody][Required] CompanyUpdateDto companyDto)
        {
            if (companyDto == null)
            {
                return BadRequest(new { message = "Request body is required" });
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var result = await _companyService.UpdateCompanyAsync(id, companyDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var result = await _companyService.DeleteCompanyAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/companies/bulk-delete
        [HttpPost("bulk-delete")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> BulkDeleteCompanies([FromBody][Required] BulkDeleteRequestDto request)
        {
            if (request == null || request.Ids == null || request.Ids.Count == 0)
            {
                return BadRequest(new { message = "Ids list is required" });
            }

            var deleted = await _companyService.BulkDeleteCompaniesAsync(request.Ids);
            return Ok(new { deletedCount = deleted });
        }
    }
} 