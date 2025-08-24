using AutoMapper;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.Industry;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
	public class IndustryService : IIndustryService
	{
		private readonly IIndustryRepository _industryRepository;
		private readonly IMapper _mapper;

		public IndustryService(IIndustryRepository industryRepository, IMapper mapper)
		{
			_industryRepository = industryRepository;
			_mapper = mapper;
		}

		public async Task<PagedResult<IndustryResponseDto>> GetFilteredAsync(IndustryQueryDto queryDto)
		{
			var result = await _industryRepository.GetFilteredAsync(queryDto);
			return new PagedResult<IndustryResponseDto>
			{
				Items = _mapper.Map<List<IndustryResponseDto>>(result.Items),
				TotalCount = result.TotalCount,
				PageSize = result.PageSize,
				PageNumber = result.PageNumber
			};
		}

		public async Task<IEnumerable<IndustryResponseDto>> GetAllAsync()
		{
			var list = await _industryRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<IndustryResponseDto>>(list);
		}

		public async Task<IndustryResponseDto?> GetByIdAsync(int id)
		{
			var entity = await _industryRepository.GetByIdAsync(id);
			return entity == null ? null : _mapper.Map<IndustryResponseDto>(entity);
		}
	}
}


