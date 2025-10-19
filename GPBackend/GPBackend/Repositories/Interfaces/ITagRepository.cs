using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(int id);
        Task<Tag?> GetByNameAsync(string name);
        Task<Tag> CreateAsync(Tag tag);
        Task<IEnumerable<Tag>> GetOrCreateTagsAsync(List<string> tagNames);
        Task<IEnumerable<Tag>> SearchTagsAsync(string searchTerm);
    }
}

