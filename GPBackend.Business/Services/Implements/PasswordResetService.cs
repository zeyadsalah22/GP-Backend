using System.Security.Cryptography;
using System.Text;
using GPBackend.DTOs.Email;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GPBackend.Services.Implements
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _tokenRepository;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PasswordResetService> _logger;

        public PasswordResetService(
            IUserRepository userRepository,
            IPasswordResetTokenRepository tokenRepository,
            IEmailService emailService,
            IConfiguration configuration,
            ILogger<PasswordResetService> logger)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _emailService = emailService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task RequestPasswordResetAsync(string email, string? ip, string? userAgent)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                // Avoid user enumeration; just return
                return;
            }

            // Generate token and hash
            var rawBytes = RandomNumberGenerator.GetBytes(_configuration.GetValue<int>("PasswordResetTokenSettings:TokenLength", 32));
            var token = WebEncoders.Base64UrlEncode(rawBytes);
            var tokenHash = SHA256.HashData(Encoding.UTF8.GetBytes(token));

            var entity = new PasswordResetToken
            {
                UserId = user.UserId,
                TokenHash = tokenHash,
                ExpiresAt = DateTime.UtcNow.AddMinutes(
                    _configuration.GetValue<int>("PasswordResetTokenSettings:ExpiryMinutes", 30)),
                CreatedIp = ip,
                CreatedUserAgent = userAgent
            };

            await _tokenRepository.CreateAsync(entity);

            var resetLink = $"{_configuration["AppSettings:ClientUrl"]}/reset-password?token={token}";
            await _emailService.SendPasswordResetEmailAsync(user.Email, resetLink);
        }

        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            var tokenHash = SHA256.HashData(Encoding.UTF8.GetBytes(token));
            var record = await _tokenRepository.GetActiveByHashAsync(tokenHash);
            if (record == null || record.ExpiresAt <= DateTime.UtcNow)
            {
                return false;
            }

            var user = await _userRepository.GetByIdAsync(record.UserId);
            if (user == null)
            {
                return false;
            }

            // Hash new password (consider stronger hashing in production)
            string hashed = HashPassword(newPassword);
            var changed = await _userRepository.ChangePasswordAsync(user.UserId, hashed);
            if (!changed)
            {
                return false;
            }

            await _tokenRepository.MarkUsedAsync(record.Id);
            return true;
        }

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
    }
}


