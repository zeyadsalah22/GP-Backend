using GPBackend.DTOs.Post;
using GPBackend.DTOs.Common;

namespace GPBackend.Services.Interfaces
{
    public interface IPostService
    {
        Task<PagedResult<PostResponseDto>> GetFilteredPostsAsync(PostQueryDto queryDto);
        Task<IEnumerable<PostResponseDto>> GetAllPostsAsync();
        Task<PostResponseDto?> GetPostByIdAsync(int id);
        Task<PostResponseDto> CreatePostAsync(int userId, PostCreateDto postDto);
        Task<bool> UpdatePostAsync(int id, int userId, PostUpdateDto postDto);
        Task<bool> DeletePostAsync(int id, int userId);
        Task<int> BulkDeletePostsAsync(IEnumerable<int> ids, int userId);
        Task<IEnumerable<PostResponseDto>> GetPostsByUserIdAsync(int userId);
        Task<IEnumerable<PostResponseDto>> GetPublishedPostsAsync();
        Task<IEnumerable<PostResponseDto>> GetDraftsByUserIdAsync(int userId);
    }
}

