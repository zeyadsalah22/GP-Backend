using GPBackend.Models;
using System.Security.Cryptography;

namespace GPBackend.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
        int? ValidateToken(string token);
        DateTime GetTokenExpiry(string token);
    }
} 