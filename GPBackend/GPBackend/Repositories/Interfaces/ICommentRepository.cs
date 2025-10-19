using GPBackend.Models;
using GPBackend.DTOs.Comment;
using GPBackend.DTOs.Common;

namespace GPBackend.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<PagedResult<Comment>> GetCommentsByPostIdAsync(CommentQueryDto queryDto);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> GetByIdWithRepliesAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<bool> UpdateAsync(Comment comment);
        Task<bool> DeleteAsync(int id);
        Task<int> GetCommentCountByPostIdAsync(int postId);
        Task<List<Comment>> GetTopCommentsByPostIdAsync(int postId, int count);
        Task<List<Comment>> GetRepliesByCommentIdAsync(int commentId);
        Task<bool> UpdateReplyCountAsync(int commentId, int increment);
        Task<int> SoftDeleteAllByPostIdAsync(int postId);
        Task AddCommentEditHistoryAsync(CommentEditHistory history);
        Task AddCommentMentionsAsync(List<CommentMention> mentions);
        Task RemoveCommentMentionsAsync(int commentId);
    }
}
