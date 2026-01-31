using AutoMapper;
using GPBackend.DTOs.Reaction;
using GPBackend.Models;
using GPBackend.Models.Enums;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Services.Implements
{
    public class PostReactionService : IPostReactionService
    {
        private readonly IPostReactionRepository _postReactionRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICommunityNotificationService _notificationService;

        private const int UPVOTE_POINTS = 10;
        private const int HELPFUL_POINTS = 15;
        private const int INSIGHTFUL_POINTS = 12;
        private const int DOWNVOTE_POINTS = 0;
        private const int THANKS_POINTS = 0;

        public PostReactionService(
            IPostReactionRepository postReactionRepository,
            IPostRepository postRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ICommunityNotificationService notificationService)
        {
            _postReactionRepository = postReactionRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<PostReactionResponseDto> AddOrUpdateReactionAsync(int userId, PostReactionCreateDto reactionDto)
        {
            var post = await _postRepository.GetByIdAsync(reactionDto.PostId);
            if (post == null)
                throw new KeyNotFoundException($"Post with ID {reactionDto.PostId} not found.");

            var existingReaction = await _postReactionRepository.GetByPostAndUserAsync(reactionDto.PostId, userId);

            if (existingReaction != null)
            {
                if (existingReaction.ReactionType == reactionDto.ReactionType)
                {
                    await RemoveReputationPoints(post.UserId, existingReaction.ReactionType);

                    await _postReactionRepository.DeleteAsync(existingReaction);
                    throw new InvalidOperationException("Reaction removed successfully.");
                }
                else
                {
                    await RemoveReputationPoints(post.UserId, existingReaction.ReactionType);
                    await AddReputationPoints(post.UserId, reactionDto.ReactionType);

                    existingReaction.ReactionType = reactionDto.ReactionType;
                    existingReaction.UpdatedAt = DateTime.UtcNow;
                    var updatedReaction = await _postReactionRepository.UpdateAsync(existingReaction);

                    var responseDto = _mapper.Map<PostReactionResponseDto>(updatedReaction);
                    responseDto.ReactionTypeName = updatedReaction.ReactionType.ToString();
                    return responseDto;
                }
            }

            var newReaction = new PostReaction
            {
                PostId = reactionDto.PostId,
                UserId = userId,
                ReactionType = reactionDto.ReactionType,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await AddReputationPoints(post.UserId, reactionDto.ReactionType);

            var addedReaction = await _postReactionRepository.AddAsync(newReaction);
            
            // Notify post owner about the reaction
            await _notificationService.NotifyPostReactionAsync(reactionDto.PostId, userId);

            var result = _mapper.Map<PostReactionResponseDto>(addedReaction);
            result.ReactionTypeName = addedReaction.ReactionType.ToString();
            return result;
        }

        public async Task RemoveReactionAsync(int userId, int postId)
        {
            var existingReaction = await _postReactionRepository.GetByPostAndUserAsync(postId, userId);
            if (existingReaction == null)
                throw new KeyNotFoundException("Reaction not found.");

            var post = await _postRepository.GetByIdAsync(postId);
            if (post != null)
            {
                await RemoveReputationPoints(post.UserId, existingReaction.ReactionType);
            }

            await _postReactionRepository.DeleteAsync(existingReaction);
        }

        public async Task<PostReactionSummaryDto> GetReactionSummaryAsync(int postId, int? currentUserId = null)
        {
            var reactionCounts = await _postReactionRepository.GetReactionCountsByPostIdAsync(postId);

            var summary = new PostReactionSummaryDto
            {
                UpvoteCount = reactionCounts[ReactionType.UPVOTE],
                DownvoteCount = reactionCounts[ReactionType.DOWNVOTE],
                HelpfulCount = reactionCounts[ReactionType.HELPFUL],
                InsightfulCount = reactionCounts[ReactionType.INSIGHTFUL],
                ThanksCount = reactionCounts[ReactionType.THANKS],
                TotalReactions = reactionCounts.Values.Sum()
            };

            if (currentUserId.HasValue)
            {
                var userReaction = await _postReactionRepository.GetByPostAndUserAsync(postId, currentUserId.Value);
                summary.UserReaction = userReaction?.ReactionType.ToString();
            }

            return summary;
        }

        public async Task<GroupedWhoReactedDto> GetWhoReactedGroupedAsync(int postId)
        {
            var reactions = await _postReactionRepository.GetByPostIdAsync(postId);

            var grouped = reactions
                .GroupBy(r => r.ReactionType)
                .OrderByDescending(g => g.Count())
                .ToDictionary(
                    g => g.Key.ToString(),
                    g => g.OrderByDescending(r => r.CreatedAt)
                        .Select(r => new ReactionUserDto
                        {
                            UserId = r.UserId,
                            UserName = $"{r.User.Fname} {r.User.Lname}",
                            ProfilePictureUrl = r.User.ProfilePictureUrl,
                            ReactionType = r.ReactionType,
                            ReactionTypeName = r.ReactionType.ToString(),
                            ReactedAt = r.CreatedAt
                        }).ToList()
                );

            return new GroupedWhoReactedDto
            {
                ReactionsByType = grouped,
                TotalCount = reactions.Count
            };
        }

        public async Task<WhoReactedDto> GetWhoReactedByTypeAsync(int postId, ReactionType reactionType, int pageNumber = 1, int pageSize = 50)
        {
            var allReactions = await _postReactionRepository.GetByPostIdAsync(postId);
            var filteredReactions = allReactions.Where(r => r.ReactionType == reactionType).ToList();

            var totalCount = filteredReactions.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginatedReactions = filteredReactions
                .OrderByDescending(r => r.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new ReactionUserDto
                {
                    UserId = r.UserId,
                    UserName = $"{r.User.Fname} {r.User.Lname}",
                    ProfilePictureUrl = r.User.ProfilePictureUrl,
                    ReactionType = r.ReactionType,
                    ReactionTypeName = r.ReactionType.ToString(),
                    ReactedAt = r.CreatedAt
                })
                .ToList();

            return new WhoReactedDto
            {
                Reactions = paginatedReactions,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        private async Task AddReputationPoints(int userId, ReactionType reactionType)
        {
            var points = GetReputationPoints(reactionType);
            if (points > 0)
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user != null)
                {
                    user.ReputationPoints += points;
                    await _userRepository.UpdateAsync(user);
                }
            }
        }

        private async Task RemoveReputationPoints(int userId, ReactionType reactionType)
        {
            var points = GetReputationPoints(reactionType);
            if (points > 0)
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user != null)
                {
                    user.ReputationPoints = Math.Max(0, user.ReputationPoints - points);
                    await _userRepository.UpdateAsync(user);
                }
            }
        }

        private int GetReputationPoints(ReactionType reactionType)
        {
            return reactionType switch
            {
                ReactionType.UPVOTE => UPVOTE_POINTS,
                ReactionType.HELPFUL => HELPFUL_POINTS,
                ReactionType.INSIGHTFUL => INSIGHTFUL_POINTS,
                ReactionType.DOWNVOTE => DOWNVOTE_POINTS,
                ReactionType.THANKS => THANKS_POINTS,
                _ => 0
            };
        }
    }
}

