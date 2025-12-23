using GPBackend.DTOs.Reaction;

namespace GPBackend.Services.Interfaces
{
    public interface ICommentReactionService
    {
        Task<CommentReactionResponseDto> AddOrUpdateReactionAsync(int userId, CommentReactionCreateDto reactionDto);
        Task RemoveReactionAsync(int userId, int commentId);
        Task<CommentReactionSummaryDto> GetReactionSummaryAsync(int commentId, int? currentUserId = null);
    }
}

