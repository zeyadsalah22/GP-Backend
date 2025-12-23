namespace GPBackend.Services.Interfaces
{
    public interface IPasswordResetService
    {
        Task RequestPasswordResetAsync(string email, string? ip, string? userAgent);
        Task<bool> ResetPasswordAsync(string token, string newPassword);
    }
}


