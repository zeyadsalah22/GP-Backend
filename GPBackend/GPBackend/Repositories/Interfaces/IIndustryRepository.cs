using GPBackend.DTOs.Common;
using GPBackend.DTOs.Industry;
using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
	public interface IIndustryRepository
	{
		Task<PagedResult<Industry>> GetFilteredAsync(IndustryQueryDto queryDto);
		Task<IEnumerable<Industry>> GetAllAsync();
		Task<Industry?> GetByIdAsync(int id);
	}
}


