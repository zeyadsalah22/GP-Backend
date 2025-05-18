using GPBackend.Models;

namespace GPBackend.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        int? ValidateToken(string token);
    }
} 