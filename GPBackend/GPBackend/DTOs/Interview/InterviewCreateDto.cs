using System.ComponentModel.DataAnnotations;
using GPBackend.Models;

namespace GPBackend.DTOs.Interview
{
    public class InterviewValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is InterviewCreateDto dto)
            {
                // If ApplicationId is provided, validation passes
                if (dto.ApplicationId.HasValue && dto.ApplicationId.Value > 0)
                {
                    return true;
                }

                // If ApplicationId is not provided, both Position and JobDescription must be provided
                if (!string.IsNullOrWhiteSpace(dto.Position) && !string.IsNullOrWhiteSpace(dto.JobDescription))
                {
                    return true;
                }

                return false;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Either ApplicationId must be provided, or both Position and JobDescription must be provided.";
        }
    }

    [InterviewValidation]
    public class InterviewCreateDto
    {
        public int? ApplicationId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
        public int? CompanyId { get; set; }
        public string? Position { get; set; }
        public string? JobDescription { get; set; }
        


    }
}