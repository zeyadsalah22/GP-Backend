using System;

namespace GPBackend.DTOs
{
    public class StatisticsDTO
    {
        public int total_applications { get; set; }
        public int rejected_applications { get; set; }
        public int pending_applications { get; set; }
        public int accepted_applications { get; set; }
        public DateTime? last_application { get; set; }
        public DateTime? last_rejection { get; set; }
        public DateTime? last_acceptance { get; set; }
        public DateTime? last_pending { get; set; }
    }
} 