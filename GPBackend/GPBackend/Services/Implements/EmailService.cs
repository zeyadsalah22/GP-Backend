using GPBackend.DTOs.Email;
using GPBackend.Services.Interfaces;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GPBackend.Services.Implements{
    public class EmailService : IEmailService{
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        private readonly IGmailApiEmailSender _gmailSender;
        
        public EmailService(
            IConfiguration configuration,
            ILogger<EmailService> logger,
            IGmailApiEmailSender gmailSender)
        {
            _configuration = configuration;
            _logger = logger;
            _gmailSender = gmailSender;
        }

        public async Task<bool> SendWelcomeEmailAsync(WelcomeEmailDto welcomeEmailDto){
            var emailDto = new EmailDto{
                To = welcomeEmailDto.Email,
                Subject = "Welcome to Job Lander " + welcomeEmailDto.FirstName,
                Body = GenerateWelcomeEmailTemplate(welcomeEmailDto),
                IsHtml = true,
            };
            return await SendEmailAsync(emailDto);
        }

        public async Task<bool> SendEmailAsync(EmailDto emailDto){
            try
            {
                var email = new MimeMessage();
                var senderName = _configuration["GmailSender:SenderName"] ?? "Job Lander";
                var senderEmail = _configuration["GmailSender:SenderEmail"] ?? throw new InvalidOperationException("GmailSender:SenderEmail is not configured");
                email.From.Add(new MailboxAddress(senderName, senderEmail));
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
                
                await _gmailSender.SendAsync(email);

                _logger.LogInformation("Email sent successfully to {Email}", emailDto.To);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", emailDto.To);
                return false;
            }
        }

        public async Task<bool> SendPasswordResetEmailAsync(string email, string resetLink){
            var subject = "Reset your Job Lander password";
            var body = $@"
                <html>
                <body style='font-family: Arial, sans-serif; color: #333;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <h2 style='color: #2c3e50;'>Password Reset Request</h2>
                        <p>We received a request to reset your password. Click the button below to proceed:</p>
                        <div style='margin: 24px 0;'>
                            <a href='{resetLink}'
                               style='background:#3498db;color:#fff;padding:12px 20px;border-radius:6px;text-decoration:none;display:inline-block;'>
                                Reset Password
                            </a>
                        </div>
                        <p style='color:#666'>This link will expire soon. If you didn't request this, you can safely ignore this email.</p>
                        <hr style='border:none;border-top:1px solid #eee;margin-top:28px'>
                        <p style='color:#999;font-size:12px'>This is an automated message. Please do not reply.</p>
                    </div>
                </body>
                </html>";

            var emailDto = new EmailDto{
                To = email,
                Subject = subject,
                Body = body,
                IsHtml = true
            };
            return await SendEmailAsync(emailDto);
        }
        public async Task<bool> SendNotificationEmailAsync(EmailDto emailDto)
        {
            // reformat body into html body
            // emailDto.Body = 
            return await SendEmailAsync(emailDto);
        }

        private string GenerateWelcomeEmailTemplate(WelcomeEmailDto welcomeEmailDto){
            return $@"
                <html>
                <body style='font-family: Arial, sans-serif; color: #333;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <div style='text-align: center; margin-bottom: 30px;'>
                            <h1 style='color: #2c3e50; margin-bottom: 10px;'>Welcome to Job Lander!</h1>
                            <div style='width: 50px; height: 3px; background-color: #3498db; margin: 0 auto;'></div>
                        </div>
                        
                        <div style='background-color: #f8f9fa; padding: 25px; border-radius: 8px; margin-bottom: 25px;'>
                            <h2 style='color: #2c3e50; margin-top: 0;'>
                                Hello {welcomeEmailDto.FirstName} {welcomeEmailDto.LastName}!
                            </h2>
                            <p style='font-size: 16px; line-height: 1.6;'>
                                Thank you for joining Job Lander. We're excited to have you on board!
                            </p>
                        </div>
                        
                        <div style='margin-bottom: 25px;'>
                            <h3 style='color: #2c3e50;'>What's next?</h3>
                            <ul style='line-height: 1.8;'>
                                <li>Complete your profile to get personalized recommendations</li>
                                <li>Explore our features and tools</li>
                                <li>Connect with other users in your industry</li>
                                <li>Start building your professional network</li>
                                 </ul>
                        </div>
                        
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='{_configuration["AppSettings:ClientUrl"]}/dashboard' 
                               style='background-color: #3498db; color: white; padding: 15px 30px; 
                                      text-decoration: none; border-radius: 5px; display: inline-block;
                                      font-weight: bold;'>
                                Get Started
                            </a>
                        </div>
                        
                        <div style='background-color: #ecf0f1; padding: 20px; border-radius: 5px; margin-top: 30px;'>
                            <p style='margin: 0; color: #666; text-align: center;'>
                                <strong>Registration Details:</strong><br>
                                Email: {welcomeEmailDto.Email}<br>
                                Registered on: {welcomeEmailDto.RegistrationDate:MMMM dd, yyyy}
                            </p>
                        </div>
                        
                        <hr style='margin-top: 30px; border: none; border-top: 1px solid #eee;'>
                        <p style='color: #999; font-size: 12px; text-align: center;'>
                            This is an automated message, please do not reply to this email.<br>
                            If you have any questions, please contact our support team.
                        </p>
                    </div>
                </body>
                </html>";
        }
    }
}