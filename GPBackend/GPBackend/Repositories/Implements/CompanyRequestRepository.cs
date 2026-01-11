using GPBackend.DTOs.Common;
using GPBackend.DTOs.CompanyRequest;
using GPBackend.Models;
using GPBackend.Models.Enums;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class CompanyRequestRepository : ICompanyRequestRepository
    {
        private readonly GPDBContext _context;

        public CompanyRequestRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<int> CreateRequestAsync(CompanyRequest request)
        {
            _context.CompanyRequests.Add(request);
            await _context.SaveChangesAsync();
            return request.RequestId;
        }

        public async Task<CompanyRequest?> GetRequestByIdAsync(int requestId)
        {
            return await _context.CompanyRequests
                .Include(cr => cr.User)
                .Include(cr => cr.ReviewedByAdmin)
                .Include(cr => cr.Industry)
                .Where(cr => !cr.IsDeleted && cr.RequestId == requestId)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<CompanyRequest>> GetFilteredRequestsAsync(CompanyRequestQueryDto queryDto)
        {
            // Start with base query
            IQueryable<CompanyRequest> query = _context.CompanyRequests
                .Include(cr => cr.User)
                .Include(cr => cr.ReviewedByAdmin)
                .Include(cr => cr.Industry)
                .Where(cr => !cr.IsDeleted);

            // Apply filters
            if (queryDto.UserId.HasValue)
            {
                query = query.Where(cr => cr.UserId == queryDto.UserId.Value);
            }

            if (queryDto.RequestStatus.HasValue)
            {
                query = query.Where(cr => cr.RequestStatus == queryDto.RequestStatus.Value);
            }

            if (!string.IsNullOrWhiteSpace(queryDto.CompanyName))
            {
                string companyName = queryDto.CompanyName.ToLower();
                query = query.Where(cr => cr.CompanyName.ToLower().Contains(companyName));
            }

            if (queryDto.IndustryId.HasValue)
            {
                query = query.Where(cr => cr.IndustryId == queryDto.IndustryId.Value);
            }

            if (queryDto.FromDate.HasValue)
            {
                query = query.Where(cr => cr.RequestedAt >= queryDto.FromDate.Value);
            }

            if (queryDto.ToDate.HasValue)
            {
                query = query.Where(cr => cr.RequestedAt <= queryDto.ToDate.Value);
            }

            // Apply search term (search across multiple fields)
            if (!string.IsNullOrWhiteSpace(queryDto.SearchTerm))
            {
                string searchTerm = queryDto.SearchTerm.ToLower();
                query = query.Where(cr =>
                    cr.CompanyName.ToLower().Contains(searchTerm) ||
                    (cr.Location != null && cr.Location.ToLower().Contains(searchTerm)) ||
                    (cr.Description != null && cr.Description.ToLower().Contains(searchTerm))
                );
            }

            // Get total count before pagination
            int totalCount = await query.CountAsync();

            // Apply sorting
            query = ApplySorting(query, queryDto.SortBy ?? "RequestedAt", queryDto.SortDescending);

            // Apply pagination
            var items = await query
                .Skip((queryDto.PageNumber - 1) * queryDto.PageSize)
                .Take(queryDto.PageSize)
                .ToListAsync();

            // Create and return paged result
            return new PagedResult<CompanyRequest>
            {
                Items = items,
                PageNumber = queryDto.PageNumber,
                PageSize = queryDto.PageSize,
                TotalCount = totalCount
            };
        }

        private IQueryable<CompanyRequest> ApplySorting(IQueryable<CompanyRequest> query, string sortBy, bool descending)
        {
            // Convert the sortBy parameter to match property names
            sortBy = char.ToUpper(sortBy[0]) + sortBy.Substring(1);

            return sortBy switch
            {
                "RequestedAt" => descending ? query.OrderByDescending(cr => cr.RequestedAt) : query.OrderBy(cr => cr.RequestedAt),
                "CompanyName" => descending ? query.OrderByDescending(cr => cr.CompanyName) : query.OrderBy(cr => cr.CompanyName),
                "RequestStatus" => descending ? query.OrderByDescending(cr => cr.RequestStatus) : query.OrderBy(cr => cr.RequestStatus),
                "ReviewedAt" => descending ? query.OrderByDescending(cr => cr.ReviewedAt) : query.OrderBy(cr => cr.ReviewedAt),
                _ => descending ? query.OrderByDescending(cr => cr.RequestedAt) : query.OrderBy(cr => cr.RequestedAt) // Default to RequestedAt
            };
        }

        public async Task<IEnumerable<CompanyRequest>> GetUserRequestsAsync(int userId)
        {
            return await _context.CompanyRequests
                .Include(cr => cr.User)
                .Include(cr => cr.ReviewedByAdmin)
                .Include(cr => cr.Industry)
                .Where(cr => !cr.IsDeleted && cr.UserId == userId)
                .OrderByDescending(cr => cr.RequestedAt)
                .ToListAsync();
        }

        public async Task<bool> UpdateRequestStatusAsync(int requestId, CompanyRequestStatus status, int adminId, string? reason)
        {
            var request = await _context.CompanyRequests
                .Where(cr => !cr.IsDeleted && cr.RequestId == requestId)
                .FirstOrDefaultAsync();

            if (request == null)
            {
                return false;
            }

            request.RequestStatus = status;
            request.ReviewedAt = DateTime.UtcNow;
            request.ReviewedByAdminId = adminId;
            
            if (status == CompanyRequestStatus.Rejected && !string.IsNullOrWhiteSpace(reason))
            {
                request.RejectionReason = reason;
            }

            _context.CompanyRequests.Update(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CompanyRequest?> CheckDuplicateRequestAsync(string companyName)
        {
            // Fuzzy match: check for exact match or very similar names
            string normalizedName = companyName.Trim().ToLower();
            
            return await _context.CompanyRequests
                .Where(cr => !cr.IsDeleted && 
                            cr.CompanyName.ToLower() == normalizedName &&
                            cr.RequestStatus == CompanyRequestStatus.Pending)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> RequestExistsAsync(int requestId)
        {
            return await _context.CompanyRequests
                .AnyAsync(cr => !cr.IsDeleted && cr.RequestId == requestId);
        }
    }
}

