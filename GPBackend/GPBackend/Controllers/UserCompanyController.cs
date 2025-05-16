using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.UserCompany;
using GPBackend.DTOs.Common;

namespace GPBackend.Controllers
{
    [ApiController]
    [Route("api/user-companies")]
    public class UserCompanyController : ControllerBase
    {
        private readonly IUserCompanyService _userCompanyService;

        public UserCompanyController(IUserCompanyService userCompanyService)
        {
            _userCompanyService = userCompanyService;
        }

        // GET: api/user-companies
        // Can be filtered with query params:
        // - ?userId={userId} - Filter by user ID
        // - ?companyId={companyId} - Filter by company ID
        // - ?companyName={name} - Filter by company name (partial match)
        // - ?searchTerm={term} - Search in company name, description, user first/last name
        // - ?sortBy={field}&sortDescending={true|false} - Sort results
        // - ?pageNumber={num}&pageSize={size} - Paginate results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCompanyResponseDto>>> GetUserCompanies([FromQuery] UserCompanyQueryDto queryDto)
        {
            var result = await _userCompanyService.GetFilteredUserCompaniesAsync(queryDto);
                
            // Add pagination headers
            Response.Headers.Add("X-Pagination-TotalCount", result.TotalCount.ToString());
            Response.Headers.Add("X-Pagination-PageSize", result.PageSize.ToString());
            Response.Headers.Add("X-Pagination-CurrentPage", result.PageNumber.ToString());
            Response.Headers.Add("X-Pagination-TotalPages", result.TotalPages.ToString());
            Response.Headers.Add("X-Pagination-HasNext", result.HasNext.ToString());
            Response.Headers.Add("X-Pagination-HasPrevious", result.HasPrevious.ToString());

            return Ok(result);
        }

        // GET: api/user-companies/{userId}/{companyId}
        [HttpGet("{userId}/{companyId}")]
        public async Task<ActionResult<UserCompanyResponseDto>> GetUserCompanyById(int userId, int companyId)
        {
            var userCompany = await _userCompanyService.GetUserCompanyByIdAsync(userId, companyId);
            if (userCompany == null)
            {
                return NotFound();
            }
            return Ok(userCompany);
        }

        // POST: api/user-companies
        [HttpPost]
        public async Task<ActionResult<UserCompanyResponseDto>> CreateUserCompany(UserCompanyCreateDto userCompanyDto)
        {
            var createdUserCompany = await _userCompanyService.CreateUserCompanyAsync(userCompanyDto);
            return CreatedAtAction(
                nameof(GetUserCompanyById), 
                new { userId = createdUserCompany.UserId, companyId = createdUserCompany.CompanyId }, 
                createdUserCompany
            );
        }

        // PUT: api/user-companies/{userId}/{companyId}
        [HttpPut("{userId}/{companyId}")]
        public async Task<IActionResult> UpdateUserCompany(int userId, int companyId, UserCompanyUpdateDto userCompanyDto)
        {
            var result = await _userCompanyService.UpdateUserCompanyAsync(userId, companyId, userCompanyDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/user-companies/{userId}/{companyId}
        [HttpDelete("{userId}/{companyId}")]
        public async Task<IActionResult> DeleteUserCompany(int userId, int companyId)
        {
            var result = await _userCompanyService.DeleteUserCompanyAsync(userId, companyId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
} 