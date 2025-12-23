using GPBackend.Models;
using GPBackend.Models.Enums;

namespace GPBackend.Repositories.Interfaces
{
    public interface IPostReactionRepository
    {
        Task<PostReaction?> GetByIdAsync(int postReactionId);
        Task<PostReaction?> GetByPostAndUserAsync(int postId, int userId);
        Task<List<PostReaction>> GetByPostIdAsync(int postId);
        Task<List<PostReaction>> GetByPostIdWithUsersAsync(int postId, int pageNumber, int pageSize);
        Task<Dictionary<ReactionType, int>> GetReactionCountsByPostIdAsync(int postId);
        Task<int> GetTotalReactionCountByPostIdAsync(int postId);
        Task<PostReaction> AddAsync(PostReaction postReaction);
        Task<PostReaction> UpdateAsync(PostReaction postReaction);
        Task DeleteAsync(PostReaction postReaction);
        Task<bool> ExistsAsync(int postId, int userId);
    }
}

