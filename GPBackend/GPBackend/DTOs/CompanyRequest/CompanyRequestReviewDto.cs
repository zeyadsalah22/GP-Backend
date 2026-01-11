using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.CompanyRequest
{
    public class CompanyRequestReviewDto
    {
        [StringLength(1000, ErrorMessage = "Rejection reason cannot exceed 1000 characters")]
        public string? RejectionReason { get; set; }
    }
}

