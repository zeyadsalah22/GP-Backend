using GPBackend.DTOs.Auth;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GPBackend.Services.Implements
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public RefreshTokenService(
            IRefreshTokenRepository refreshTokenRepository,
            IJwtService jwtService,
            IUserService userService,
            IConfiguration configuration)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken)
        {
            var existingToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            
            if (existingToken == null || !existingToken.IsActive)
            {
                return null;
            }

            // Mark the old refresh token as used
            existingToken.IsUsed = true;
            
            // Generate new tokens
            var newAccessToken = _jwtService.GenerateToken(existingToken.User);
            var newRefreshTokenString = _jwtService.GenerateRefreshToken();
            
            // Create new refresh token
            var newRefreshToken = new RefreshToken
            {
                Token = newRefreshTokenString,
                UserId = existingToken.UserId,
                ExpiryDate = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JwtSettings:RefreshTokenExpiryInDays"] ?? "7")),
                CreatedAt = DateTime.UtcNow
            };

            // Update old token and create new one
            existingToken.ReplacedByToken = newRefreshTokenString;
            await _refreshTokenRepository.UpdateAsync(existingToken);
            await _refreshTokenRepository.CreateAsync(newRefreshToken);

            return new AuthResponseDto
            {
                Token = newAccessToken,
                RefreshToken = newRefreshTokenString,
                UserId = existingToken.User.UserId,
                Email = existingToken.User.Email,
                FullName = $"{existingToken.User.Fname} {existingToken.User.Lname}",
                Role = existingToken.User.Role,
                ExpiresAt = _jwtService.GetTokenExpiry(newAccessToken)
            };
        }

        public async Task<string> GenerateRefreshTokenAsync(User user)
        {
            var refreshToken = new RefreshToken
            {
                Token = _jwtService.GenerateRefreshToken(),
                UserId = user.UserId,
                ExpiryDate = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JwtSettings:RefreshTokenExpiryInDays"] ?? "7")),
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepository.CreateAsync(refreshToken);
            return refreshToken.Token;
        }

        public async Task<bool> RevokeRefreshTokenAsync(string refreshToken)
        {
            return await _refreshTokenRepository.RevokeTokenAsync(refreshToken);
        }

        public async Task<bool> RevokeAllUserRefreshTokensAsync(int userId)
        {
            return await _refreshTokenRepository.RevokeAllUserTokensAsync(userId);
        }

        public async Task CleanupExpiredTokensAsync()
        {
            await _refreshTokenRepository.CleanupExpiredTokensAsync();
        }
    }
} 