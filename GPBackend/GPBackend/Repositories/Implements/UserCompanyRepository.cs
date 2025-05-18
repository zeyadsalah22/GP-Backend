using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.UserCompany;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GPBackend.Repositories.Implements
{
    public class UserCompanyRepository : IUserCompanyRepository
    {
        private readonly GPDBContext _context;

        public UserCompanyRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<UserCompany>> GetFilteredAsync(UserCompanyQueryDto queryDto)
        {
            // Start with base query
            IQueryable<UserCompany> query = _context.UserCompanies
                .Where(uc => !uc.IsDeleted)
                .Include(uc => uc.Company)
                .Include(uc => uc.User);

            // Apply search
            if (!string.IsNullOrWhiteSpace(queryDto.SearchTerm))
            {
                string searchTerm = queryDto.SearchTerm.ToLower();
                query = query.Where(uc => 
                    uc.Company.Name.ToLower().Contains(searchTerm) ||
                    (uc.Description != null && uc.Description.ToLower().Contains(searchTerm)) ||
                    uc.User.Fname.ToLower().Contains(searchTerm) ||
                    uc.User.Lname.ToLower().Contains(searchTerm)
                );
            }
            
            // Apply filters
            if (queryDto.UserId.HasValue)
            {
                query = query.Where(uc => uc.UserId == queryDto.UserId.Value);
            }

            if (queryDto.CompanyId.HasValue)
            {
                query = query.Where(uc => uc.CompanyId == queryDto.CompanyId.Value);
            }
            
            if (!string.IsNullOrWhiteSpace(queryDto.CompanyName))
            {
                string companyName = queryDto.CompanyName.ToLower();
                query = query.Where(uc => uc.Company.Name.ToLower().Contains(companyName));
            }

            // Get total count before pagination
            int totalCount = await query.CountAsync();

            // Apply sorting
            string sortBy = queryDto.SortBy ?? "CreatedAt";
            bool descending = queryDto.SortDescending;
            query = ApplySorting(query, sortBy, descending);

            // Apply pagination
            int pageSize = queryDto.PageSize;
            int pageNumber = queryDto.PageNumber;
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<UserCompany>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        private IQueryable<UserCompany> ApplySorting(IQueryable<UserCompany> query, string sortBy, bool descending)
        {
            return sortBy switch
            {
                "CompanyName" => descending ? 
                    query.OrderByDescending(uc => uc.Company.Name) : 
                    query.OrderBy(uc => uc.Company.Name),
                "UserName" => descending ? 
                    query.OrderByDescending(uc => uc.User.Fname) : 
                    query.OrderBy(uc => uc.User.Fname),
                "CreatedAt" => descending ? 
                    query.OrderByDescending(uc => uc.CreatedAt) : 
                    query.OrderBy(uc => uc.CreatedAt),
                "UpdatedAt" => descending ? 
                    query.OrderByDescending(uc => uc.UpdatedAt) : 
                    query.OrderBy(uc => uc.UpdatedAt),
                _ => descending ? 
                    query.OrderByDescending(uc => uc.CreatedAt) : 
                    query.OrderBy(uc => uc.CreatedAt) // Default to CreatedAt
            };
        }

        public async Task<IEnumerable<UserCompany>> GetAllAsync()
        {
            return await _context.UserCompanies
                .Where(uc => !uc.IsDeleted)
                .Include(uc => uc.Company)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserCompany>> GetByUserIdAsync(int userId)
        {
            return await _context.UserCompanies
                .Where(uc => !uc.IsDeleted && uc.UserId == userId)
                .Include(uc => uc.Company)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserCompany>> GetByCompanyIdAsync(int companyId)
        {
            return await _context.UserCompanies
                .Where(uc => !uc.IsDeleted && uc.CompanyId == companyId)
                .Include(uc => uc.User)
                .ToListAsync();
        }

        public async Task<UserCompany?> GetByIdAsync(int userId, int companyId)
        {
            return await _context.UserCompanies
                .Where(uc => !uc.IsDeleted && uc.UserId == userId && uc.CompanyId == companyId)
                .Include(uc => uc.Company)
                .FirstOrDefaultAsync();
        }

        public async Task<UserCompany> CreateAsync(UserCompany userCompany)
        {
            _context.UserCompanies.Add(userCompany);
            await _context.SaveChangesAsync();
            return userCompany;
        }

        public async Task<bool> UpdateAsync(UserCompany userCompany)
        {
            try
            {
                _context.UserCompanies.Update(userCompany);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserCompanyExistsAsync(userCompany.UserId, userCompany.CompanyId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int userId, int companyId)
        {
            var userCompany = await GetByIdAsync(userId, companyId);
            if (userCompany == null)
            {
                return false;
            }

            // Soft delete
            userCompany.IsDeleted = true;
            userCompany.UpdatedAt = DateTime.UtcNow;
            _context.UserCompanies.Update(userCompany);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserCompanyExistsAsync(int userId, int companyId)
        {
            return await _context.UserCompanies.AnyAsync(uc => 
                !uc.IsDeleted && uc.UserId == userId && uc.CompanyId == companyId);
        }
    }
} 