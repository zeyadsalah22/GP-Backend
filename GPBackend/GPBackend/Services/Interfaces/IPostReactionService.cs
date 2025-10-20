using GPBackend.DTOs.Reaction;
using GPBackend.Models.Enums;

namespace GPBackend.Services.Interfaces
{
    public interface IPostReactionService
    {
        Task<PostReactionResponseDto> AddOrUpdateReactionAsync(int userId, PostReactionCreateDto reactionDto);
        Task RemoveReactionAsync(int userId, int postId);
        Task<PostReactionSummaryDto> GetReactionSummaryAsync(int postId, int? currentUserId = null);
        Task<GroupedWhoReactedDto> GetWhoReactedGroupedAsync(int postId);
        Task<WhoReactedDto> GetWhoReactedByTypeAsync(int postId, ReactionType reactionType, int pageNumber = 1, int pageSize = 50);
    }
}

