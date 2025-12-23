using AutoMapper;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Post;

namespace GPBackend.Services.Implements
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
        {
            var tags = await _tagRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }

        public async Task<TagDto?> GetTagByIdAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            return tag != null ? _mapper.Map<TagDto>(tag) : null;
        }

        public async Task<IEnumerable<TagDto>> SearchTagsAsync(string searchTerm)
        {
            var tags = await _tagRepository.SearchTagsAsync(searchTerm);
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }
    }
}

