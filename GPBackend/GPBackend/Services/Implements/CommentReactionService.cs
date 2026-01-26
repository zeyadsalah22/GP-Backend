using AutoMapper;
using GPBackend.DTOs.Reaction;
using GPBackend.Models;
using GPBackend.Models.Enums;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class CommentReactionService : ICommentReactionService
    {
        private readonly ICommentReactionRepository _commentReactionRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICommunityNotificationService _notificationService;

        private const int UPVOTE_POINTS = 5;
        // private const int DOWNVOTE_POINTS = 0;

        public CommentReactionService(
            ICommentReactionRepository commentReactionRepository,
            ICommentRepository commentRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ICommunityNotificationService notificationService)
        {
            _commentReactionRepository = commentReactionRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<CommentReactionResponseDto> AddOrUpdateReactionAsync(int userId, CommentReactionCreateDto reactionDto)
        {
            var comment = await _commentRepository.GetByIdAsync(reactionDto.CommentId);
            if (comment == null)
                throw new KeyNotFoundException($"Comment with ID {reactionDto.CommentId} not found.");

            var existingReaction = await _commentReactionRepository.GetByCommentAndUserAsync(reactionDto.CommentId, userId);

            if (existingReaction != null)
            {
                // toggle if the same
                if (existingReaction.ReactionType == reactionDto.ReactionType)
                {
                    await RemoveReputationPoints(comment.UserId, existingReaction.ReactionType);

                    await _commentReactionRepository.DeleteAsync(existingReaction);
                    throw new InvalidOperationException("Reaction removed successfully.");
                }
                else
                {
                    await RemoveReputationPoints(comment.UserId, existingReaction.ReactionType);
                    await AddReputationPoints(comment.UserId, reactionDto.ReactionType);

                    existingReaction.ReactionType = reactionDto.ReactionType;
                    existingReaction.UpdatedAt = DateTime.UtcNow;
                    var updatedReaction = await _commentReactionRepository.UpdateAsync(existingReaction);

                    var responseDto = _mapper.Map<CommentReactionResponseDto>(updatedReaction);
                    responseDto.ReactionTypeName = updatedReaction.ReactionType.ToString();
                    return responseDto;
                }
            }

            var newReaction = new CommentReaction
            {
                CommentId = reactionDto.CommentId,
                UserId = userId,
                ReactionType = reactionDto.ReactionType,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await AddReputationPoints(comment.UserId, reactionDto.ReactionType);

            var addedReaction = await _commentReactionRepository.AddAsync(newReaction);
            
            // Notify comment owner about the reaction
            await _notificationService.NotifyCommentReactionAsync(reactionDto.CommentId, userId);

            var result = _mapper.Map<CommentReactionResponseDto>(addedReaction);
            result.ReactionTypeName = addedReaction.ReactionType.ToString();
            return result;
        }

        public async Task RemoveReactionAsync(int userId, int commentId)
        {
            var existingReaction = await _commentReactionRepository.GetByCommentAndUserAsync(commentId, userId);
            if (existingReaction == null)
                throw new KeyNotFoundException("Reaction not found.");

            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment != null)
            {
                await RemoveReputationPoints(comment.UserId, existingReaction.ReactionType);
            }

            await _commentReactionRepository.DeleteAsync(existingReaction);
        }

        public async Task<CommentReactionSummaryDto> GetReactionSummaryAsync(int commentId, int? currentUserId = null)
        {
            var upvoteCount = await _commentReactionRepository.GetUpvoteCountByCommentIdAsync(commentId);
            var downvoteCount = await _commentReactionRepository.GetDownvoteCountByCommentIdAsync(commentId);

            var summary = new CommentReactionSummaryDto
            {
                UpvoteCount = upvoteCount,
                DownvoteCount = downvoteCount,
                Score = upvoteCount - downvoteCount
            };

            if (currentUserId.HasValue)
            {
                var userReaction = await _commentReactionRepository.GetByCommentAndUserAsync(commentId, currentUserId.Value);
                summary.UserReaction = userReaction?.ReactionType.ToString();
            }

            return summary;
        }

        private async Task AddReputationPoints(int userId, CommentReactionType reactionType)
        {
            if (reactionType == CommentReactionType.UPVOTE)
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user != null)
                {
                    user.ReputationPoints += UPVOTE_POINTS;
                    await _userRepository.UpdateAsync(user);
                }
            }
        }

        private async Task RemoveReputationPoints(int userId, CommentReactionType reactionType)
        {
            if (reactionType == CommentReactionType.UPVOTE)
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user != null)
                {
                    user.ReputationPoints = Math.Max(0, user.ReputationPoints - UPVOTE_POINTS);
                    await _userRepository.UpdateAsync(user);
                }
            }
        }
    }
}

