using AutoMapper;
using GPBackend.DTOs.SavedPost;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class SavedPostService : ISavedPostService
    {
        private readonly ISavedPostRepository _savedPostRepository;
        private readonly IPostRepository _postRepository;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public SavedPostService(
            ISavedPostRepository savedPostRepository,
            IPostRepository postRepository,
            IPostService postService,
            IMapper mapper)
        {
            _savedPostRepository = savedPostRepository;
            _postRepository = postRepository;
            _postService = postService;
            _mapper = mapper;
        }

        public async Task<SavedPostResponseDto> SavePostAsync(int userId, int postId)
        {
            // Check if post exists and is published
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null || post.IsDeleted || post.Status != Models.Enums.PostStatus.PUBLISHED)
            {
                throw new InvalidOperationException("Post not found or is not available for saving");
            }

            // Check if already saved
            var existingSavedPost = await _savedPostRepository.GetByUserAndPostAsync(userId, postId);
            if (existingSavedPost != null)
            {
                return _mapper.Map<SavedPostResponseDto>(existingSavedPost);
            }

            // Create new saved post
            var savedPost = new SavedPost
            {
                UserId = userId,
                PostId = postId,
                SavedAt = DateTime.UtcNow
            };

            var createdSavedPost = await _savedPostRepository.AddAsync(savedPost);

            return await MapSavedPostResponseAsync(createdSavedPost);
        }

        public async Task<bool> UnsavePostAsync(int userId, int postId)
        {
            var savedPost = await _savedPostRepository.GetByUserAndPostAsync(userId, postId);
            if (savedPost == null)
            {
                return false;
            }

            await _savedPostRepository.DeleteAsync(savedPost);
            return true;
        }

        public async Task<List<SavedPostResponseDto>> GetUserSavedPostsAsync(int userId)
        {
            var savedPosts = await _savedPostRepository.GetByUserIdAsync(userId);
            var result = new List<SavedPostResponseDto>(savedPosts.Count);

            foreach (var savedPost in savedPosts)
            {
                result.Add(await MapSavedPostResponseAsync(savedPost));
            }

            return result;
        }

        public async Task<bool> IsPostSavedByUserAsync(int userId, int postId)
        {
            return await _savedPostRepository.ExistsAsync(userId, postId);
        }

        private async Task<SavedPostResponseDto> MapSavedPostResponseAsync(SavedPost savedPost)
        {
            var dto = _mapper.Map<SavedPostResponseDto>(savedPost);
            dto.Post = await _postService.GetPostByIdAsync(savedPost.PostId);
            return dto;
        }
    }
}

