using GPBackend.DTOs.Common;

namespace GPBackend.DTOs.ResumeTest
{
    public class ResumeTestQueryDto : PaginationQueryDto
    {
        public int? ResumeId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public double? MinScore { get; set; }
        public double? MaxScore { get; set; }

    }
} 