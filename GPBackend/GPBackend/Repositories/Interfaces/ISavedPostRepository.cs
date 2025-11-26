using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface ISavedPostRepository
    {
        Task<SavedPost?> GetByIdAsync(int savedPostId);
        Task<SavedPost?> GetByUserAndPostAsync(int userId, int postId);
        Task<List<SavedPost>> GetByUserIdAsync(int userId);
        Task<SavedPost> AddAsync(SavedPost savedPost);
        Task DeleteAsync(SavedPost savedPost);
        Task<bool> ExistsAsync(int userId, int postId);
    }
}

