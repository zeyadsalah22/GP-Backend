using GPBackend.Models;
using GPBackend.DTOs.Post;
using GPBackend.DTOs.Common;

namespace GPBackend.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<PagedResult<Post>> GetFilteredAsync(PostQueryDto queryDto);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(int id);
        Task<Post> CreateAsync(Post post);
        Task<bool> UpdateAsync(Post post);
        Task<bool> DeleteAsync(int id);
        Task<int> BulkSoftDeleteAsync(IEnumerable<int> ids);
        Task<IEnumerable<Post>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Post>> GetPublishedPostsAsync();
        Task<IEnumerable<Post>> GetDraftsByUserIdAsync(int userId);
    }
}

