using MimeKit;

namespace GPBackend.Services.Interfaces
{
    public interface IGmailApiEmailSender
    {
        Task SendAsync(MimeMessage message, CancellationToken cancellationToken = default);
    }
}


