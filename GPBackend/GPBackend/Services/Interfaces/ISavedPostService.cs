using GPBackend.DTOs.SavedPost;

namespace GPBackend.Services.Interfaces
{
    public interface ISavedPostService
    {
        Task<SavedPostResponseDto> SavePostAsync(int userId, int postId);
        Task<bool> UnsavePostAsync(int userId, int postId);
        Task<List<SavedPostResponseDto>> GetUserSavedPostsAsync(int userId);
        Task<bool> IsPostSavedByUserAsync(int userId, int postId);
    }
}

