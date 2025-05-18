using GPBackend.DTOs.Auth;
using GPBackend.DTOs.User;
using GPBackend.Models;

namespace GPBackend.Services.Interfaces
{
    public interface IUserService
    {
        // Authentication methods
        Task<User?> AuthenticateAsync(string email, string password);
        Task<bool> EmailExistsAsync(string email);
        Task<int> RegisterUserAsync(RegisterDto registerDto);
        
        // User management methods
        Task<UserResponseDto?> GetUserByIdAsync(int id);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
        Task<bool> ChangePasswordAsync(int id, string currentPassword, string newPassword);
        Task<bool> DeleteUserAsync(int id);
    }
} 