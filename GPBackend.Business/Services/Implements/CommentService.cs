using AutoMapper;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Comment;
using GPBackend.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Services.Implements
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly GPDBContext _context;

        public CommentService(
            ICommentRepository commentRepository,
            IPostRepository postRepository,
            IMapper mapper,
            GPDBContext context)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<PagedResult<CommentResponseDto>> GetCommentsByPostIdAsync(CommentQueryDto queryDto)
        {
            var pagedComments = await _commentRepository.GetCommentsByPostIdAsync(queryDto);

            var dtoItems = pagedComments.Items.Select(c => MapToResponseDto(c)).ToList();

            return new PagedResult<CommentResponseDto>
            {
                Items = dtoItems,
                PageNumber = pagedComments.PageNumber,
                PageSize = pagedComments.PageSize,
                TotalCount = pagedComments.TotalCount
            };
        }

        public async Task<CommentResponseDto?> GetCommentByIdAsync(int id)
        {
            var comment = await _commentRepository.GetByIdWithRepliesAsync(id);
            return comment != null ? MapToResponseDto(comment) : null;
        }

        public async Task<CommentResponseDto> CreateCommentAsync(int userId, CommentCreateDto commentDto)
        {
            var post = await _postRepository.GetByIdAsync(commentDto.PostId);
            if (post == null)
            {
                throw new InvalidOperationException("Post not found");
            }

            Comment? parentComment = null; // validate parent comment if this is a reply
            int level = 0;

            if (commentDto.ParentCommentId.HasValue)
            {
                parentComment = await _commentRepository.GetByIdAsync(commentDto.ParentCommentId.Value);
                if (parentComment == null)
                {
                    throw new InvalidOperationException("Parent comment not found");
                }

                // enforce max 2 levels
                if (parentComment.Level >= 1)
                {
                    throw new InvalidOperationException("Cannot reply to a reply. Maximum nesting level is 1.");
                }

                level = parentComment.Level + 1;
            }

            var comment = new Comment
            {
                PostId = commentDto.PostId,
                UserId = userId,
                ParentCommentId = commentDto.ParentCommentId,
                Content = commentDto.Content,
                Level = level,
                ReplyCount = 0,
                IsEdited = false,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdComment = await _commentRepository.CreateAsync(comment);

            if (commentDto.MentionedUserIds != null && commentDto.MentionedUserIds.Any())
            {
                var mentions = commentDto.MentionedUserIds.Distinct().Select(mentionedUserId => new CommentMention
                {
                    CommentId = createdComment.CommentId,
                    MentionedUserId = mentionedUserId,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

                await _commentRepository.AddCommentMentionsAsync(mentions);
            }

            if (commentDto.ParentCommentId.HasValue)
            {
                await _commentRepository.UpdateReplyCountAsync(commentDto.ParentCommentId.Value, 1);
            }

            if (level == 0)
            {
                post.CommentCount++;
                await _postRepository.UpdateAsync(post);
            }

            var reloadedComment = await _commentRepository.GetByIdAsync(createdComment.CommentId);
            return MapToResponseDto(reloadedComment!);
        }

        public async Task<CommentResponseDto> UpdateCommentAsync(int id, int userId, CommentUpdateDto commentDto)
        {
            var existingComment = await _commentRepository.GetByIdAsync(id);
            if (existingComment == null)
            {
                throw new InvalidOperationException("Comment not found");
            }

            if (existingComment.UserId != userId)
            {
                throw new UnauthorizedAccessException("You can only edit your own comments");
            }

            if (existingComment.IsDeleted)
            {
                throw new InvalidOperationException("Cannot edit a deleted comment");
            }

            var editHistory = new CommentEditHistory
            {
                CommentId = existingComment.CommentId,
                PreviousContent = existingComment.Content,
                EditedAt = DateTime.UtcNow
            };
            await _commentRepository.AddCommentEditHistoryAsync(editHistory);

            existingComment.Content = commentDto.Content;
            existingComment.IsEdited = true;
            existingComment.LastEditedAt = DateTime.UtcNow;
            existingComment.UpdatedAt = DateTime.UtcNow;
            // existingComment.Rowversion = commentDto.Rowversion;

            await _commentRepository.UpdateAsync(existingComment);

            await _commentRepository.RemoveCommentMentionsAsync(id);
            if (commentDto.MentionedUserIds != null && commentDto.MentionedUserIds.Any())
            {
                var mentions = commentDto.MentionedUserIds.Distinct().Select(mentionedUserId => new CommentMention
                {
                    CommentId = id,
                    MentionedUserId = mentionedUserId,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

                await _commentRepository.AddCommentMentionsAsync(mentions);
            }

            var reloadedComment = await _commentRepository.GetByIdAsync(id);
            return MapToResponseDto(reloadedComment!);
        }

        public async Task<bool> DeleteCommentAsync(int id, int userId)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return false;
            }

            if (comment.UserId != userId)
            {
                throw new UnauthorizedAccessException("You can only delete your own comments");
            }

            var result = await _commentRepository.DeleteAsync(id);

            if (result)
            {
                if (comment.ParentCommentId.HasValue)
                {
                    await _commentRepository.UpdateReplyCountAsync(comment.ParentCommentId.Value, -1);
                }

                if (comment.Level == 0)
                {
                    var post = await _postRepository.GetByIdAsync(comment.PostId);
                    if (post != null)
                    {
                        post.CommentCount--;
                        if (post.CommentCount < 0) post.CommentCount = 0;
                        await _postRepository.UpdateAsync(post);
                    }
                }
            }

            return result;
        }

        public async Task<List<CommentPreviewDto>> GetTopCommentPreviewsAsync(int postId, int count = 2)
        {
            var comments = await _commentRepository.GetTopCommentsByPostIdAsync(postId, count);

            return comments.Select(c => new CommentPreviewDto
            {
                CommentId = c.CommentId,
                AuthorName = c.IsDeleted ? "[Deleted]" : $"{c.User.Fname} {c.User.Lname}",
                ContentSnippet = c.IsDeleted ? "[Deleted comment]" : TruncateContent(c.Content, 100),
                TimeAgo = GetRelativeTime(c.CreatedAt),
                IsDeleted = c.IsDeleted
            }).ToList();
        }

        public async Task<List<string>> SearchUsersForMentionAsync(string searchTerm, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<string>();
            }

            var users = await _context.Users
                .Where(u => !u.IsDeleted &&
                    (u.Fname.ToLower().Contains(searchTerm.ToLower()) ||
                     u.Lname.ToLower().Contains(searchTerm.ToLower()) ||
                     u.Email.ToLower().Contains(searchTerm.ToLower())))
                .Take(limit)
                .Select(u => $"{u.Fname} {u.Lname}")
                .ToListAsync();

            return users;
        }

        private CommentResponseDto MapToResponseDto(Comment comment)
        {
            var dto = new CommentResponseDto
            {
                CommentId = comment.CommentId,
                PostId = comment.PostId,
                UserId = comment.UserId,
                AuthorName = comment.IsDeleted ? "[Deleted]" : $"{comment.User.Fname} {comment.User.Lname}",
                ParentCommentId = comment.ParentCommentId,
                Content = comment.IsDeleted ? "[Deleted comment]" : comment.Content,
                Level = comment.Level,
                ReplyCount = comment.ReplyCount,
                IsEdited = comment.IsEdited,
                LastEditedAt = comment.LastEditedAt,
                EditedTimeAgo = comment.IsEdited && comment.LastEditedAt.HasValue
                    ? $"edited {GetRelativeTime(comment.LastEditedAt.Value)}"
                    : null,
                IsDeleted = comment.IsDeleted,
                CreatedAt = comment.CreatedAt,
                TimeAgo = GetRelativeTime(comment.CreatedAt),
                UpdatedAt = comment.UpdatedAt,
                // Rowversion = comment.Rowversion,
                Mentions = comment.Mentions?.Select(m => new CommentMentionDto
                {
                    UserId = m.MentionedUserId,
                    UserName = $"{m.MentionedUser.Fname} {m.MentionedUser.Lname}"
                }).ToList() ?? new List<CommentMentionDto>()
            };

            if (comment.ParentComment != null)
            {
                dto.ParentAuthorName = comment.ParentComment.IsDeleted
                    ? "[Deleted]"
                    : $"{comment.ParentComment.User.Fname} {comment.ParentComment.User.Lname}";
                dto.ParentContentPreview = comment.ParentComment.IsDeleted
                    ? "[Deleted comment]"
                    : TruncateContent(comment.ParentComment.Content, 100);
            }

            if (comment.Replies != null && comment.Replies.Any())
            {
                dto.Replies = comment.Replies.Select(r => MapToResponseDto(r)).ToList();
            }

            return dto;
        }

        private string GetRelativeTime(DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime;

            if (timeSpan.TotalSeconds < 60)
                return "just now";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} minute{((int)timeSpan.TotalMinutes != 1 ? "s" : "")} ago";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hour{((int)timeSpan.TotalHours != 1 ? "s" : "")} ago";
            if (timeSpan.TotalDays < 7)
                return $"{(int)timeSpan.TotalDays} day{((int)timeSpan.TotalDays != 1 ? "s" : "")} ago";
            if (timeSpan.TotalDays < 30)
                return $"{(int)(timeSpan.TotalDays / 7)} week{((int)(timeSpan.TotalDays / 7) != 1 ? "s" : "")} ago";
            if (timeSpan.TotalDays < 365)
                return $"{(int)(timeSpan.TotalDays / 30)} month{((int)(timeSpan.TotalDays / 30) != 1 ? "s" : "")} ago";

            return $"{(int)(timeSpan.TotalDays / 365)} year{((int)(timeSpan.TotalDays / 365) != 1 ? "s" : "")} ago";
        }

        private string TruncateContent(string content, int maxLength)
        {
            if (string.IsNullOrEmpty(content) || content.Length <= maxLength)
                return content;

            return content.Substring(0, maxLength) + "...";
        }
    }
}
