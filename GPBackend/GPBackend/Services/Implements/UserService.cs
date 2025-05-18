using AutoMapper;
using GPBackend.DTOs.Auth;
using GPBackend.DTOs.User;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace GPBackend.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return null;

            // Hash the provided password and compare with stored hash
            if (!VerifyPasswordHash(password, user.Password))
                return null;

            return user;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.EmailExistsAsync(email);
        }

        public async Task<int> RegisterUserAsync(RegisterDto registerDto)
        {
            // Create a new user using mapper
            var user = _mapper.Map<User>(registerDto);
            
            // Set password and other fields not in DTO
            user.Password = HashPassword(registerDto.Password);

            return await _userRepository.CreateAsync(user);
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? _mapper.Map<UserResponseDto>(user) : null;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<bool> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
        {
            // First get the existing user
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return false;

            // Map the update DTO to the existing user
            _mapper.Map(userUpdateDto, existingUser);
            
            // Update timestamp
            existingUser.UpdatedAt = DateTime.UtcNow;
            
            // We don't update the password or security-related fields here
            return await _userRepository.UpdateAsync(existingUser);
        }

        public async Task<bool> ChangePasswordAsync(int id, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            // Verify current password
            if (!VerifyPasswordHash(currentPassword, user.Password))
                return false;

            // Hash the new password
            string hashedPassword = HashPassword(newPassword);
            
            // Update password using repository
            return await _userRepository.ChangePasswordAsync(id, hashedPassword);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        #region Password Hashing

        private string HashPassword(string password)
        {
            // In a real production environment, you should use a more secure password hashing 
            // algorithm like BCrypt or Argon2. This is a simple implementation for demonstration.
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            string hashedPassword = HashPassword(password);
            return hashedPassword == storedHash;
        }

        #endregion
    }
} 