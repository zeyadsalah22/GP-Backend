using AutoMapper;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Post;
using GPBackend.DTOs.Common;

namespace GPBackend.Services.Implements
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPostReactionRepository _postReactionRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, ITagRepository tagRepository, ICommentRepository commentRepository, IPostReactionRepository postReactionRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _commentRepository = commentRepository;
            _postReactionRepository = postReactionRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PostResponseDto>> GetFilteredPostsAsync(PostQueryDto queryDto)
        {
            var pagedPosts = await _postRepository.GetFilteredAsync(queryDto);

            //var dtoItems = pagedPosts.Items.Select(p => MapToResponseDto(p)).ToList();

            var dtoItems = new List<PostResponseDto>();
            foreach (var post in pagedPosts.Items)
            {
                dtoItems.Add(await MapToResponseDtoAsync(post, includeCommentPreviews: true));
            }

            return new PagedResult<PostResponseDto>
            {
                Items = dtoItems,
                PageNumber = pagedPosts.PageNumber,
                PageSize = pagedPosts.PageSize,
                TotalCount = pagedPosts.TotalCount
            };
        }

        public async Task<IEnumerable<PostResponseDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            // return posts.Select(p => MapToResponseDto(p));
            var dtos = new List<PostResponseDto>();
            foreach (var post in posts)
            {
                dtos.Add(await MapToResponseDtoAsync(post, includeCommentPreviews: true));
            }
            return dtos;
        }

        public async Task<PostResponseDto?> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            //return post != null ? MapToResponseDto(post) : null;
            return post != null ? await MapToResponseDtoAsync(post, includeCommentPreviews: false) : null;
        }

        public async Task<PostResponseDto> CreatePostAsync(int userId, PostCreateDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            post.UserId = userId;
            post.CreatedAt = DateTime.UtcNow;
            post.UpdatedAt = DateTime.UtcNow;

            var createdPost = await _postRepository.CreateAsync(post);

            // Handle tags if provided
            if (postDto.Tags != null && postDto.Tags.Any())
            {
                var tags = await _tagRepository.GetOrCreateTagsAsync(postDto.Tags);
                foreach (var tag in tags)
                {
                    var postTag = new PostTag
                    {
                        PostId = createdPost.PostId,
                        TagId = tag.TagId,
                        CreatedAt = DateTime.UtcNow
                    };
                    createdPost.PostTags.Add(postTag);
                }
                await _postRepository.UpdateAsync(createdPost);
            }

            // Reload to get all navigation properties
            var reloadedPost = await _postRepository.GetByIdAsync(createdPost.PostId);
            // return MapToResponseDto(reloadedPost!);
            return await MapToResponseDtoAsync(reloadedPost!, includeCommentPreviews: false);
        }

        public async Task<bool> UpdatePostAsync(int id, int userId, PostUpdateDto postDto)
        {
            var existingPost = await _postRepository.GetByIdAsync(id);
            if (existingPost == null || existingPost.UserId != userId)
            {
                return false;
            }

            // Update basic fields
            if (postDto.PostType.HasValue)
                existingPost.PostType = postDto.PostType.Value;

            if (postDto.Title != null)
                existingPost.Title = postDto.Title;

            if (postDto.Content != null)
                existingPost.Content = postDto.Content;

            if (postDto.IsAnonymous.HasValue)
                existingPost.IsAnonymous = postDto.IsAnonymous.Value;

            if (postDto.Status.HasValue)
                existingPost.Status = postDto.Status.Value;

            // Update tags if provided
            if (postDto.Tags != null)
            {
                existingPost.PostTags.Clear();
                if (postDto.Tags.Any())
                {
                    var tags = await _tagRepository.GetOrCreateTagsAsync(postDto.Tags);
                    foreach (var tag in tags)
                    {
                        var postTag = new PostTag
                        {
                            PostId = existingPost.PostId,
                            TagId = tag.TagId,
                            CreatedAt = DateTime.UtcNow
                        };
                        existingPost.PostTags.Add(postTag);
                    }
                }
            }

            existingPost.UpdatedAt = DateTime.UtcNow;
            return await _postRepository.UpdateAsync(existingPost);
        }

        public async Task<bool> DeletePostAsync(int id, int userId)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null || post.UserId != userId)
            {
                return false;
            }
            return await _postRepository.DeleteAsync(id);
        }

        public async Task<int> BulkDeletePostsAsync(IEnumerable<int> ids, int userId)
        {
            // Filter to only delete posts owned by the user
            var posts = await _postRepository.GetAllAsync();
            var userPostIds = posts
                .Where(p => ids.Contains(p.PostId) && p.UserId == userId)
                .Select(p => p.PostId);

            return await _postRepository.BulkSoftDeleteAsync(userPostIds);
        }

        public async Task<IEnumerable<PostResponseDto>> GetPostsByUserIdAsync(int userId)
        {
            var posts = await _postRepository.GetByUserIdAsync(userId);
            // return posts.Select(p => MapToResponseDto(p));
            var dtos = new List<PostResponseDto>();
            foreach (var post in posts)
            {
                dtos.Add(await MapToResponseDtoAsync(post, includeCommentPreviews: true));
            }
            return dtos;
        }

        public async Task<IEnumerable<PostResponseDto>> GetPublishedPostsAsync()
        {
            var posts = await _postRepository.GetPublishedPostsAsync();
            // return posts.Select(p => MapToResponseDto(p));
            var dtos = new List<PostResponseDto>();
            foreach (var post in posts)
            {
                dtos.Add(await MapToResponseDtoAsync(post, includeCommentPreviews: true));
            }
            return dtos;
        }

        public async Task<IEnumerable<PostResponseDto>> GetDraftsByUserIdAsync(int userId)
        {
            var posts = await _postRepository.GetDraftsByUserIdAsync(userId);
            // return posts.Select(p => MapToResponseDto(p));
            var dtos = new List<PostResponseDto>();
            foreach (var post in posts)
            {
                dtos.Add(await MapToResponseDtoAsync(post, includeCommentPreviews: false));
            }
            return dtos;
        }

        //private PostResponseDto MapToResponseDto(Post post)
        //{
        //    var dto = new PostResponseDto
        //    {
        //        PostId = post.PostId,
        //        UserId = post.IsAnonymous ? null : post.UserId,
        //        AuthorName = post.IsAnonymous ? null : $"{post.User?.Fname} {post.User?.Lname}",
        //        PostType = post.PostType,
        //        PostTypeName = post.PostType.ToString(),
        //        Title = post.Title,
        //        Content = post.Content,
        //        IsAnonymous = post.IsAnonymous,
        //        Status = post.Status,
        //        StatusName = post.Status.ToString(),
        //        Tags = post.PostTags.Select(pt => new TagDto
        //        {
        //            TagId = pt.Tag.TagId,
        //            Name = pt.Tag.Name
        //        }).ToList(),
        //        CreatedAt = post.CreatedAt,
        //        RelativeTime = GetRelativeTime(post.CreatedAt),
        //        UpdatedAt = post.UpdatedAt,
        //        Rowversion = post.Rowversion
        //    };

        //    return dto;
        //}

        private async Task<PostResponseDto> MapToResponseDtoAsync(Post post, bool includeCommentPreviews = false)
        {
            var dto = new PostResponseDto
            {
                PostId = post.PostId,
                UserId = post.IsAnonymous ? null : post.UserId,
                AuthorName = post.IsAnonymous ? null : $"{post.User?.Fname} {post.User?.Lname}",
                PostType = post.PostType,
                PostTypeName = post.PostType.ToString(),
                Title = post.Title,
                Content = post.Content,
                ContentExcerpt = TruncateContent(post.Content, 200),
                IsAnonymous = post.IsAnonymous,
                Status = post.Status,
                StatusName = post.Status.ToString(),
                Tags = post.PostTags.Select(pt => new TagDto
                {
                    TagId = pt.Tag.TagId,
                    Name = pt.Tag.Name
                }).ToList(),
                CommentCount = post.CommentCount,
                CreatedAt = post.CreatedAt,
                RelativeTime = GetRelativeTime(post.CreatedAt),
                UpdatedAt = post.UpdatedAt,
                Rowversion = post.Rowversion,
                AuthorProfilePictureUrl = post.User?.ProfilePictureUrl
            };

            var reactionCounts = await _postReactionRepository.GetReactionCountsByPostIdAsync(post.PostId);
            dto.UpvoteCount = reactionCounts[Models.Enums.ReactionType.UPVOTE];
            dto.DownvoteCount = reactionCounts[Models.Enums.ReactionType.DOWNVOTE];
            dto.HelpfulCount = reactionCounts[Models.Enums.ReactionType.HELPFUL];
            dto.InsightfulCount = reactionCounts[Models.Enums.ReactionType.INSIGHTFUL];
            dto.ThanksCount = reactionCounts[Models.Enums.ReactionType.THANKS];
            dto.TotalReactions = reactionCounts.Values.Sum();

            // Load comment previews if requested (for feed)
            if (includeCommentPreviews)
            {
                var commentPreviews = await _commentRepository.GetTopCommentsByPostIdAsync(post.PostId, 2);
                dto.CommentPreviews = commentPreviews.Select(c => new DTOs.Comment.CommentPreviewDto
                {
                    CommentId = c.CommentId,
                    AuthorName = c.IsDeleted ? "[Deleted]" : $"{c.User.Fname} {c.User.Lname}",
                    ContentSnippet = c.IsDeleted ? "[Deleted comment]" : TruncateContent(c.Content, 100),
                    TimeAgo = GetRelativeTime(c.CreatedAt),
                    IsDeleted = c.IsDeleted
                }).ToList();
            }

            return dto;
        }

        private string TruncateContent(string content, int maxLength)
        {
            if (string.IsNullOrEmpty(content) || content.Length <= maxLength)
                return content;

            return content.Substring(0, maxLength) + "...";
        }

        private string GetRelativeTime(DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime;

            if (timeSpan.TotalMinutes < 1)
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
    }
}

