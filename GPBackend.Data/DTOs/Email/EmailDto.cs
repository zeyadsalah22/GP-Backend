using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Email
{
    public class EmailDto
    {
        [Required(ErrorMessage = "Recipient email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string To { get; set; } = null!;

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; } = null!;

        [Required(ErrorMessage = "Body is required")]
        public string Body { get; set; } = null!;

        public bool IsHtml { get; set; } = true;

        public List<string>? CcEmails { get; set; }

        public List<string>? BccEmails { get; set; }
    }
}