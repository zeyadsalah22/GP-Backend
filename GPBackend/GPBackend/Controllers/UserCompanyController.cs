using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.UserCompany;
using GPBackend.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user-companies")]
    public class UserCompanyController : ControllerBase
    {
        private readonly IUserCompanyService _userCompanyService;

        public UserCompanyController(IUserCompanyService userCompanyService)
        {
            _userCompanyService = userCompanyService;
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

        // GET: api/user-companies
        // Can be filtered with query params:
        // - ?companyId={companyId} - Filter by company ID
        // - ?companyName={name} - Filter by company name (partial match)
        // - ?searchTerm={term} - Search in company name, description
        // - ?sortBy={field}&sortDescending={true|false} - Sort results
        // - ?pageNumber={num}&pageSize={size} - Paginate results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCompanyResponseDto>>> GetUserCompanies([FromQuery] UserCompanyQueryDto queryDto)
        {
            try
            {
                // Always use the authenticated user's ID
                int userId = GetAuthenticatedUserId();
                queryDto.UserId = userId;
                
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
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // GET: api/user-companies/{companyId}
        [HttpGet("{companyId}")]
        public async Task<ActionResult<UserCompanyResponseDto>> GetUserCompanyById(int companyId)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                
                var userCompany = await _userCompanyService.GetUserCompanyByIdAsync(userId, companyId);
                if (userCompany == null)
                {
                    return NotFound();
                }
                return Ok(userCompany);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // POST: api/user-companies
        [HttpPost]
        public async Task<ActionResult<UserCompanyResponseDto>> CreateUserCompany(UserCompanyCreateDto userCompanyDto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                
                // Ensure the user is creating a relationship for themselves
                userCompanyDto.UserId = userId;
                
                var createdUserCompany = await _userCompanyService.CreateUserCompanyAsync(userCompanyDto);
                return CreatedAtAction(
                    nameof(GetUserCompanyById), 
                    new { companyId = createdUserCompany.CompanyId }, 
                    createdUserCompany
                );
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // PUT: api/user-companies/{companyId}
        [HttpPut("{companyId}")]
        public async Task<IActionResult> UpdateUserCompany(int companyId, UserCompanyUpdateDto userCompanyDto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                
                var result = await _userCompanyService.UpdateUserCompanyAsync(userId, companyId, userCompanyDto);
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

        // DELETE: api/user-companies/{companyId}
        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteUserCompany(int companyId)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                
                var result = await _userCompanyService.DeleteUserCompanyAsync(userId, companyId);
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