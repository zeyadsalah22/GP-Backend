using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.DTOs.Company;
using GPBackend.DTOs.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GPBackend.Repositories.Implements
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly GPDBContext _context;

        public CompanyRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Company>> GetFilteredAsync(CompanyQueryDto queryDto)
        {
            // Start with base query
            IQueryable<Company> query = _context.Companies.Where(c => !c.IsDeleted);

            // Apply search
            if (!string.IsNullOrWhiteSpace(queryDto.SearchTerm))
            {
                string searchTerm = queryDto.SearchTerm.ToLower();
                query = query.Where(c =>
                    c.Name.ToLower().Contains(searchTerm) ||
                    c.Location != null && c.Location.ToLower().Contains(searchTerm));
            }

            // Apply filters
            if (!string.IsNullOrWhiteSpace(queryDto.Location))
            {
                string location = queryDto.Location.ToLower();
                query = query.Where(c => c.Location != null && c.Location.ToLower().Contains(location));
            }

            // Get total count before pagination
            int totalCount = await query.CountAsync();

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(queryDto.SortBy))
            {
                // Apply dynamic sorting based on the property name
                query = ApplySorting(query, queryDto.SortBy, queryDto.SortDescending);
            }
            else
            {
                // Default sort by Name ascending
                query = queryDto.SortDescending
                    ? query.OrderByDescending(c => c.Name)
                    : query.OrderBy(c => c.Name);
            }

            // Apply pagination
            var items = await query
                .Skip((queryDto.PageNumber - 1) * queryDto.PageSize)
                .Take(queryDto.PageSize)
                .ToListAsync();

            // Create and return paged result
            return new PagedResult<Company>
            {
                Items = items,
                PageNumber = queryDto.PageNumber,
                PageSize = queryDto.PageSize,
                TotalCount = totalCount
            };
        }

        private IQueryable<Company> ApplySorting(IQueryable<Company> query, string sortBy, bool descending)
        {
            // Convert the sortBy parameter to match property names (e.g., "name" -> "Name")
            sortBy = char.ToUpper(sortBy[0]) + sortBy.Substring(1).ToLower();

            return sortBy switch
            {
                "Name" => descending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
                "Location" => descending ? query.OrderByDescending(c => c.Location) : query.OrderBy(c => c.Location),
                "CreatedAt" => descending ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt),
                "UpdatedAt" => descending ? query.OrderByDescending(c => c.UpdatedAt) : query.OrderBy(c => c.UpdatedAt),
                _ => descending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name) // Default to Name
            };
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _context.Companies
                .Where(c => !c.IsDeleted && c.CompanyId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Company> CreateAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<bool> UpdateAsync(Company company)
        {
            try
            {
                _context.Companies.Update(company);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CompanyExistsAsync(company.CompanyId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var company = await GetByIdAsync(id);
            if (company == null)
            {
                return false;
            }

            // Soft delete
            company.IsDeleted = true;
            company.UpdatedAt = DateTime.UtcNow;
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> CompanyExistsAsync(int id)
        {
            return await _context.Companies.AnyAsync(c => c.CompanyId == id);
        }
    }
}