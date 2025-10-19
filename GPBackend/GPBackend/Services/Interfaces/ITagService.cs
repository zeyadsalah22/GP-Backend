using GPBackend.DTOs.Post;

namespace GPBackend.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAllTagsAsync();
        Task<TagDto?> GetTagByIdAsync(int id);
        Task<IEnumerable<TagDto>> SearchTagsAsync(string searchTerm);
    }
}

