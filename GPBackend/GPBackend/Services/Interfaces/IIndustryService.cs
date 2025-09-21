using GPBackend.DTOs.Common;
using GPBackend.DTOs.Industry;

namespace GPBackend.Services.Interfaces
{
	public interface IIndustryService
	{
		Task<PagedResult<IndustryResponseDto>> GetFilteredAsync(IndustryQueryDto queryDto);
		Task<IEnumerable<IndustryResponseDto>> GetAllAsync();
		Task<IndustryResponseDto?> GetByIdAsync(int id);
	}
}


