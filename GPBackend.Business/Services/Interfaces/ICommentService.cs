using GPBackend.DTOs.Comment;
using GPBackend.DTOs.Common;

namespace GPBackend.Services.Interfaces
{
    public interface ICommentService
    {
        Task<PagedResult<CommentResponseDto>> GetCommentsByPostIdAsync(CommentQueryDto queryDto);
        Task<CommentResponseDto?> GetCommentByIdAsync(int id);
        Task<CommentResponseDto> CreateCommentAsync(int userId, CommentCreateDto commentDto);
        Task<CommentResponseDto> UpdateCommentAsync(int id, int userId, CommentUpdateDto commentDto);
        Task<bool> DeleteCommentAsync(int id, int userId);
        Task<List<CommentPreviewDto>> GetTopCommentPreviewsAsync(int postId, int count = 2);
        Task<List<string>> SearchUsersForMentionAsync(string searchTerm, int limit = 10);
    }
}
