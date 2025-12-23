using GPBackend.Models;
using GPBackend.Models.Enums;

namespace GPBackend.Repositories.Interfaces
{
    public interface ICommentReactionRepository
    {
        Task<CommentReaction?> GetByIdAsync(int commentReactionId);
        Task<CommentReaction?> GetByCommentAndUserAsync(int commentId, int userId);
        Task<List<CommentReaction>> GetByCommentIdAsync(int commentId);
        Task<int> GetUpvoteCountByCommentIdAsync(int commentId);
        Task<int> GetDownvoteCountByCommentIdAsync(int commentId);
        Task<CommentReaction> AddAsync(CommentReaction commentReaction);
        Task<CommentReaction> UpdateAsync(CommentReaction commentReaction);
        Task DeleteAsync(CommentReaction commentReaction);
        Task<bool> ExistsAsync(int commentId, int userId);
    }
}

