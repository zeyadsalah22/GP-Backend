using GPBackend.DTOs.Email;

namespace GPBackend.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendWelcomeEmailAsync(WelcomeEmailDto welcomeEmailDto);
        Task<bool> SendEmailAsync(EmailDto emailDto);
        Task<bool> SendPasswordResetEmailAsync(string email, string token);
    }
}
