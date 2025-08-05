using GPBackend.DTOs.Email;
using GPBackend.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GPBackend.Services.Implements{
    public class EmailService : IEmailService{
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        
        public EmailService(IConfiguration configuration, ILogger<EmailService> logger){
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendWelcomeEmailAsync(WelcomeEmailDto welcomeEmailDto){
            var emailDto = new EmailDto{
                To = welcomeEmailDto.Email,
                Subject = "Welcome to Job Lander " + welcomeEmailDto.FirstName,
                Body = "Welcome to Job Lander " + welcomeEmailDto.FirstName + " " + welcomeEmailDto.LastName + "! We're excited to have you on board. Your registration was successful on " + welcomeEmailDto.RegistrationDate.ToString("MM/dd/yyyy") + ".",
            };
            return await SendEmailAsync(emailDto);
        }

        public async Task<bool> SendEmailAsync(EmailDto emailDto){
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_configuration["EmailSettings:SenderName"], _configuration["EmailSettings:SenderEmail"]));
                email.To.Add(new MailboxAddress(emailDto.To, emailDto.To));
                
                if(emailDto.CcEmails != null){
                    foreach (var ccEmail in emailDto.CcEmails){
                        email.Cc.Add(new MailboxAddress(ccEmail, ccEmail));
                    }
                }

                if(emailDto.BccEmails != null){
                    foreach (var bccEmail in emailDto.BccEmails){
                        email.Bcc.Add(new MailboxAddress(bccEmail, bccEmail));
                    }
                }

                email.Subject = emailDto.Subject;

                var body = new BodyBuilder();
                if (emailDto.IsHtml){
                    body.HtmlBody = emailDto.Body;
                }
                else{
                    body.TextBody = emailDto.Body;
                }

                email.Body = body.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:Port"]), SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                _logger.LogInformation("Email sent successfully to {Email}", emailDto.To);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", emailDto.To);
                return false;
            }
        }

        public async Task<bool> SendPasswordResetEmailAsync(string email, string token){
            var emailDto = new EmailDto{
                To = email,
                Subject = "Password Reset Request",
                Body = "Please click the link below to reset your password: " + _configuration["AppSettings:ClientUrl"] + "/reset-password?token=" + token,
            };
            return await SendEmailAsync(emailDto);
        }
    }
}