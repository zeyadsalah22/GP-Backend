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
            IQueryable<Company> query = _context.Companies
                .Include(c => c.Industry)
                .Where(c => !c.IsDeleted);

            // Apply search
            if (!string.IsNullOrWhiteSpace(queryDto.SearchTerm))
            {
                string searchTerm = queryDto.SearchTerm.ToLower();
                query = query.Where(c =>
                    c.Name.ToLower().Contains(searchTerm) ||
                    (c.Location != null && c.Location.ToLower().Contains(searchTerm)) ||
                    (c.Description != null && c.Description.ToLower().Contains(searchTerm))
                );
            }

            // Apply filters
            if (!string.IsNullOrWhiteSpace(queryDto.Location))
            {
                string location = queryDto.Location.ToLower();
                query = query.Where(c => c.Location != null && c.Location.ToLower().Contains(location));
            }

            if (queryDto.IndustryId.HasValue)
            {
                query = query.Where(c => c.IndustryId == queryDto.IndustryId.Value);
            }

            //if (queryDto.CompanySize.HasValue)
            //{
            //    query = query.Where(c => c.CompanySize == queryDto.CompanySize.Value);
            //}

            if (!string.IsNullOrWhiteSpace(queryDto.CompanySize))
            {
                query = query.Where(c => c.CompanySize == queryDto.CompanySize);
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
                "IndustryId" => descending ? query.OrderByDescending(c => c.IndustryId) : query.OrderBy(c => c.IndustryId),
                "CompanySize" => descending ? query.OrderByDescending(c => c.CompanySize) : query.OrderBy(c => c.CompanySize),
                "CreatedAt" => descending ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt),
                "UpdatedAt" => descending ? query.OrderByDescending(c => c.UpdatedAt) : query.OrderBy(c => c.UpdatedAt),
                _ => descending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name) // Default to Name
            };
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies
                .Include(c => c.Industry)
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _context.Companies
                .Include(c => c.Industry)
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
                company.UpdatedAt = DateTime.UtcNow;
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

        public async Task<int> BulkSoftDeleteAsync(IEnumerable<int> ids)
        {
            if (ids == null) return 0;
            var idList = ids.Distinct().ToList();
            if (idList.Count == 0) return 0;

            var companies = await _context.Companies
                .Where(c => idList.Contains(c.CompanyId) && !c.IsDeleted)
                .ToListAsync();

            foreach (var c in companies)
            {
                c.IsDeleted = true;
                c.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return companies.Count;
        }

        private async Task<bool> CompanyExistsAsync(int id)
        {
            return await _context.Companies.AnyAsync(c => c.CompanyId == id);
        }
    }
}