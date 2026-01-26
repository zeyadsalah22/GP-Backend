using GPBackend.DTOs.Notification;
using GPBackend.Models.Enums;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class CommunityNotificationService : ICommunityNotificationService
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationRepository _notificationRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ICommunityInterviewQuestionRepository _questionRepository;
        private readonly IInterviewAnswerRepository _answerRepository;
        private readonly IUserRepository _userRepository;

        // Batching window in hours - notifications of the same type within this window will be batched
        private const int BATCHING_WINDOW_HOURS = 24;

        public CommunityNotificationService(
            INotificationService notificationService,
            INotificationRepository notificationRepository,
            IPostRepository postRepository,
            ICommentRepository commentRepository,
            ICommunityInterviewQuestionRepository questionRepository,
            IInterviewAnswerRepository answerRepository,
            IUserRepository userRepository)
        {
            _notificationService = notificationService;
            _notificationRepository = notificationRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _userRepository = userRepository;
        }

        public async Task NotifyPostCommentAsync(int postId, int commenterId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null || post.UserId == commenterId) return; // Don't notify self

            var actor = await _userRepository.GetByIdAsync(commenterId);
            if (actor == null) return;

            var actorName = $"{actor.Fname} {actor.Lname}";

            // Check for existing unread notification to batch
            var existingNotification = await _notificationRepository.GetBatchedNotificationAsync(
                post.UserId,
                postId,
                NotificationType.Comment,
                BATCHING_WINDOW_HOURS,
                "commented on your post");

            if (existingNotification != null)
            {
                // Get the existing notification and update count
                await IncrementBatchedNotificationCountAsync(post.UserId, postId, NotificationType.Comment, "commented on your post");
            }
            else
            {
                // Create new notification
                var notificationDto = new NotificationCreateDto
                {
                    UserId = post.UserId,
                    ActorId = commenterId,
                    Type = NotificationType.Comment,
                    EntityTargetedId = postId,
                    NotificationCategory = NotificationCategory.Social,
                    Message = $"{actorName} commented on your post"
                };

                await _notificationService.CreateNotificationAsync(notificationDto);
            }
        }

        public async Task NotifyPostReactionAsync(int postId, int reactorId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null || post.UserId == reactorId) return; // Don't notify self

            var actor = await _userRepository.GetByIdAsync(reactorId);
            if (actor == null) return;

            var actorName = $"{actor.Fname} {actor.Lname}";

            // Check for existing unread notification to batch
            var existingNotification = await _notificationRepository.GetBatchedNotificationAsync(
                post.UserId,
                postId,
                NotificationType.React,
                BATCHING_WINDOW_HOURS,
                "reacted to your post");

            if (existingNotification != null)
            {
                await IncrementBatchedNotificationCountAsync(post.UserId, postId, NotificationType.React, "reacted to your post");
            }
            else
            {
                var notificationDto = new NotificationCreateDto
                {
                    UserId = post.UserId,
                    ActorId = reactorId,
                    Type = NotificationType.React,
                    EntityTargetedId = postId,
                    NotificationCategory = NotificationCategory.Social,
                    Message = $"{actorName} reacted to your post"
                };

                await _notificationService.CreateNotificationAsync(notificationDto);
            }
        }

        public async Task NotifyPostMentionsAsync(int postId, List<int> mentionedUserIds, int actorId)
        {
            if (mentionedUserIds == null || !mentionedUserIds.Any()) return;

            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null) return;

            var actor = await _userRepository.GetByIdAsync(actorId);
            if (actor == null) return;

            var actorName = $"{actor.Fname} {actor.Lname}";

            // Remove actor from mentioned users (don't notify self)
            var usersToNotify = mentionedUserIds.Where(id => id != actorId).Distinct().ToList();

            var notifications = new List<NotificationCreateDto>();

            foreach (var mentionedUserId in usersToNotify)
            {
                // Check for existing notification to batch
                var existingNotification = await _notificationRepository.GetBatchedNotificationAsync(
                    mentionedUserId,
                    postId,
                    NotificationType.Post,
                    BATCHING_WINDOW_HOURS,
                    "mentioned you in a post");

                if (existingNotification != null)
                {
                    await IncrementBatchedNotificationCountAsync(mentionedUserId, postId, NotificationType.Post, "mentioned you in a post");
                }
                else
                {
                    notifications.Add(new NotificationCreateDto
                    {
                        UserId = mentionedUserId,
                        ActorId = actorId,
                        Type = NotificationType.Post,
                        EntityTargetedId = postId,
                        NotificationCategory = NotificationCategory.Social,
                        Message = $"{actorName} mentioned you in a post"
                    });
                }
            }

            if (notifications.Any())
            {
                await _notificationService.CreateBulkNotificationsAsync(notifications);
            }
        }

        public async Task NotifyCommentMentionsAsync(int commentId, List<int> mentionedUserIds, int actorId)
        {
            if (mentionedUserIds == null || !mentionedUserIds.Any()) return;

            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null) return;

            var actor = await _userRepository.GetByIdAsync(actorId);
            if (actor == null) return;

            var actorName = $"{actor.Fname} {actor.Lname}";

            // Remove actor from mentioned users (don't notify self)
            var usersToNotify = mentionedUserIds.Where(id => id != actorId).Distinct().ToList();

            var notifications = new List<NotificationCreateDto>();

            foreach (var mentionedUserId in usersToNotify)
            {
                // Check for existing notification to batch
                var existingNotification = await _notificationRepository.GetBatchedNotificationAsync(
                    mentionedUserId,
                    commentId,
                    NotificationType.Comment,
                    BATCHING_WINDOW_HOURS,
                    "mentioned you in a comment");

                if (existingNotification != null)
                {
                    await IncrementBatchedNotificationCountAsync(mentionedUserId, commentId, NotificationType.Comment, "mentioned you in a comment");
                }
                else
                {
                    notifications.Add(new NotificationCreateDto
                    {
                        UserId = mentionedUserId,
                        ActorId = actorId,
                        Type = NotificationType.Comment,
                        EntityTargetedId = commentId,
                        NotificationCategory = NotificationCategory.Social,
                        Message = $"{actorName} mentioned you in a comment"
                    });
                }
            }

            if (notifications.Any())
            {
                await _notificationService.CreateBulkNotificationsAsync(notifications);
            }
        }

        public async Task NotifyCommentReplyAsync(int commentId, int replierId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null || comment.UserId == replierId) return; // Don't notify self

            var actor = await _userRepository.GetByIdAsync(replierId);
            if (actor == null) return;

            var actorName = $"{actor.Fname} {actor.Lname}";

            // Check for existing unread notification to batch
            var existingNotification = await _notificationRepository.GetBatchedNotificationAsync(
                comment.UserId,
                commentId,
                NotificationType.Comment,
                BATCHING_WINDOW_HOURS,
                "replied to your comment");

            if (existingNotification != null)
            {
                await IncrementBatchedNotificationCountAsync(comment.UserId, commentId, NotificationType.Comment, "replied to your comment");
            }
            else
            {
                var notificationDto = new NotificationCreateDto
                {
                    UserId = comment.UserId,
                    ActorId = replierId,
                    Type = NotificationType.Comment,
                    EntityTargetedId = commentId,
                    NotificationCategory = NotificationCategory.Social,
                    Message = $"{actorName} replied to your comment"
                };

                await _notificationService.CreateNotificationAsync(notificationDto);
            }
        }

        public async Task NotifyCommentReactionAsync(int commentId, int reactorId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null || comment.UserId == reactorId) return; // Don't notify self

            var actor = await _userRepository.GetByIdAsync(reactorId);
            if (actor == null) return;

            var actorName = $"{actor.Fname} {actor.Lname}";

            // Check for existing unread notification to batch
            var existingNotification = await _notificationRepository.GetBatchedNotificationAsync(
                comment.UserId,
                commentId,
                NotificationType.React,
                BATCHING_WINDOW_HOURS,
                "reacted to your comment");

            if (existingNotification != null)
            {
                await IncrementBatchedNotificationCountAsync(comment.UserId, commentId, NotificationType.React, "reacted to your comment");
            }
            else
            {
                var notificationDto = new NotificationCreateDto
                {
                    UserId = comment.UserId,
                    ActorId = reactorId,
                    Type = NotificationType.React,
                    EntityTargetedId = commentId,
                    NotificationCategory = NotificationCategory.Social,
                    Message = $"{actorName} reacted to your comment"
                };

                await _notificationService.CreateNotificationAsync(notificationDto);
            }
        }

        public async Task NotifyQuestionAskedThisTooAsync(int questionId, int userId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            if (question == null || question.UserId == userId) return; // Don't notify self

            var actor = await _userRepository.GetByIdAsync(userId);
            if (actor == null) return;

            var actorName = $"{actor.Fname} {actor.Lname}";

            // Check for existing unread notification to batch
            var existingNotification = await _notificationRepository.GetBatchedNotificationAsync(
                question.UserId,
                questionId,
                NotificationType.Interview,
                BATCHING_WINDOW_HOURS,
                "was asked this too");

            if (existingNotification != null)
            {
                await IncrementBatchedNotificationCountAsync(question.UserId, questionId, NotificationType.Interview, "was asked this too");
            }
            else
            {
                var notificationDto = new NotificationCreateDto
                {
                    UserId = question.UserId,
                    ActorId = userId,
                    Type = NotificationType.Interview,
                    EntityTargetedId = questionId,
                    NotificationCategory = NotificationCategory.Social,
                    Message = $"{actorName} was asked this too"
                };

                await _notificationService.CreateNotificationAsync(notificationDto);
            }
        }

        public async Task NotifyAnswerHelpfulAsync(int answerId, int userId)
        {
            var answer = await _answerRepository.GetByIdAsync(answerId);
            if (answer == null || answer.UserId == userId) return; // Don't notify self

            var actor = await _userRepository.GetByIdAsync(userId);
            if (actor == null) return;

            var actorName = $"{actor.Fname} {actor.Lname}";

            // Check for existing unread notification to batch
            var existingNotification = await _notificationRepository.GetBatchedNotificationAsync(
                answer.UserId,
                answerId,
                NotificationType.Interview,
                BATCHING_WINDOW_HOURS,
                "marked your answer as helpful");

            if (existingNotification != null)
            {
                await IncrementBatchedNotificationCountAsync(answer.UserId, answerId, NotificationType.Interview, "marked your answer as helpful");
            }
            else
            {
                var notificationDto = new NotificationCreateDto
                {
                    UserId = answer.UserId,
                    ActorId = userId,
                    Type = NotificationType.Interview,
                    EntityTargetedId = answerId,
                    NotificationCategory = NotificationCategory.Social,
                    Message = $"{actorName} marked your answer as helpful"
                };

                await _notificationService.CreateNotificationAsync(notificationDto);
            }
        }

        /// <summary>
        /// Increments the count in a batched notification message
        /// </summary>
        private async Task IncrementBatchedNotificationCountAsync(int userId, int? entityTargetedId, NotificationType type, string messagePattern)
        {
            // Find the most recent unread notification matching the pattern
            var matchingNotification = await _notificationRepository.GetBatchedNotificationAsync(
                userId, 
                entityTargetedId, 
                type, 
                BATCHING_WINDOW_HOURS, 
                messagePattern);

            if (matchingNotification != null)
            {
                // Extract current count from message (e.g., "3 new comments" or "User A commented")
                var message = matchingNotification.Message;
                var countMatch = System.Text.RegularExpressions.Regex.Match(message, @"(\d+)\s+(new|more)");
                
                int newCount = 1;
                if (countMatch.Success)
                {
                    newCount = int.Parse(countMatch.Groups[1].Value) + 1;
                }
                else
                {
                    // First time batching - convert from "User A commented" to "2 new comments"
                    newCount = 2;
                }

                // Update message with new count
                var baseMessage = GetBatchedMessageBase(type, messagePattern);
                matchingNotification.Message = $"{newCount} {baseMessage}";
                matchingNotification.CreatedAt = DateTime.UtcNow; // Update timestamp to keep it fresh

                await _notificationRepository.UpdateAsync(matchingNotification);
                
                // Send updated notification via SignalR
                var notificationDto = new NotificationResponseDto
                {
                    NotificationId = matchingNotification.NotificationId,
                    UserId = matchingNotification.UserId,
                    ActorId = matchingNotification.ActorId,
                    Type = matchingNotification.Type,
                    EntityTargetedId = matchingNotification.EntityTargetedId,
                    Message = matchingNotification.Message,
                    NotificationCategory = matchingNotification.Category,
                    IsRead = matchingNotification.IsRead,
                    CreatedAt = matchingNotification.CreatedAt,
                    IsDeleted = matchingNotification.IsDeleted
                };
                
                await _notificationService.SendNotificationToUserAsync(userId, notificationDto);
            }
        }

        /// <summary>
        /// Gets the base message for batched notifications
        /// </summary>
        private string GetBatchedMessageBase(NotificationType type, string messagePattern)
        {
            return type switch
            {
                NotificationType.Comment when messagePattern.Contains("commented") => "new comments on your post",
                NotificationType.Comment when messagePattern.Contains("replied") => "new replies to your comment",
                NotificationType.Comment when messagePattern.Contains("mentioned") => "new mentions in comments",
                NotificationType.React when messagePattern.Contains("reacted") => "new reactions",
                NotificationType.Post when messagePattern.Contains("mentioned") => "new mentions in posts",
                NotificationType.Interview when messagePattern.Contains("was asked") => "people were asked this too",
                NotificationType.Interview when messagePattern.Contains("marked") => "people marked your answer as helpful",
                _ => "new notifications"
            };
        }
    }
}
