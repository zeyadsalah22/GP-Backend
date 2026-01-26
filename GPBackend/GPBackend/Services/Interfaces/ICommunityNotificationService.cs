using GPBackend.DTOs.Notification;
using GPBackend.Models.Enums;

namespace GPBackend.Services.Interfaces
{
    /// <summary>
    /// Service for handling community-related notifications with batching support
    /// </summary>
    public interface ICommunityNotificationService
    {
        /// <summary>
        /// Notifies post creator when someone comments on their post
        /// </summary>
        Task NotifyPostCommentAsync(int postId, int commenterId);

        /// <summary>
        /// Notifies post creator when someone reacts to their post
        /// </summary>
        Task NotifyPostReactionAsync(int postId, int reactorId);

        /// <summary>
        /// Notifies users when they are mentioned in a post
        /// </summary>
        Task NotifyPostMentionsAsync(int postId, List<int> mentionedUserIds, int actorId);

        /// <summary>
        /// Notifies users when they are mentioned in a comment
        /// </summary>
        Task NotifyCommentMentionsAsync(int commentId, List<int> mentionedUserIds, int actorId);

        /// <summary>
        /// Notifies comment owner when someone replies to their comment
        /// </summary>
        Task NotifyCommentReplyAsync(int commentId, int replierId);

        /// <summary>
        /// Notifies comment owner when someone reacts to their comment
        /// </summary>
        Task NotifyCommentReactionAsync(int commentId, int reactorId);

        /// <summary>
        /// Notifies question owner when someone clicks "I was asked this too"
        /// </summary>
        Task NotifyQuestionAskedThisTooAsync(int questionId, int userId);

        /// <summary>
        /// Notifies answer owner when their answer is marked as helpful
        /// </summary>
        Task NotifyAnswerHelpfulAsync(int answerId, int userId);
    }
}
