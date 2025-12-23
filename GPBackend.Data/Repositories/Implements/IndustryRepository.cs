using GPBackend.DTOs.Common;
using GPBackend.DTOs.Industry;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
	public class IndustryRepository : IIndustryRepository
	{
		private readonly GPDBContext _context;

		public IndustryRepository(GPDBContext context)
		{
			_context = context;
		}

		public async Task<PagedResult<Industry>> GetFilteredAsync(IndustryQueryDto queryDto)
		{
			var query = _context.Industries.AsQueryable();
			query = query.Where(i => !i.IsDeleted);

			if (!string.IsNullOrWhiteSpace(queryDto.SearchTerm))
			{
				var term = queryDto.SearchTerm.ToLower();
				query = query.Where(i => i.Name.ToLower().Contains(term));
			}

			if (!string.IsNullOrWhiteSpace(queryDto.SortBy))
			{
				switch (queryDto.SortBy.ToLower())
				{
					case "name":
						query = queryDto.SortDescending ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
						break;
					case "createdat":
						query = queryDto.SortDescending ? query.OrderByDescending(i => i.CreatedAt) : query.OrderBy(i => i.CreatedAt);
						break;
					default:
						query = queryDto.SortDescending ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
						break;
				}
			}
			else
			{
				query = query.OrderBy(i => i.Name);
			}

			int total = await query.CountAsync();
			var items = await query
				.Skip((queryDto.PageNumber - 1) * queryDto.PageSize)
				.Take(queryDto.PageSize)
				.ToListAsync();

			return new PagedResult<Industry>
			{
				Items = items,
				TotalCount = total,
				PageSize = queryDto.PageSize,
				PageNumber = queryDto.PageNumber
			};
		}

		public async Task<IEnumerable<Industry>> GetAllAsync()
		{
			return await _context.Industries
				.Where(i => !i.IsDeleted)
				.OrderBy(i => i.Name)
				.ToListAsync();
		}

		public async Task<Industry?> GetByIdAsync(int id)
		{
			return await _context.Industries
				.Where(i => !i.IsDeleted && i.IndustryId == id)
				.FirstOrDefaultAsync();
		}
	}
}


