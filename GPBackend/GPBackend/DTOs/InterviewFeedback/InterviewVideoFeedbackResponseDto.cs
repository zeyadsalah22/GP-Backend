using System.Text.Json.Serialization;

namespace GPBackend.DTOs.InterviewFeedback
{
    public class InterviewVideoFeedbackResponseDto
    {
        public int InterviewId { get; set; }

        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("video_path")]
        public string VideoPath { get; set; } = string.Empty;

        public VideoMetricsDto Metrics { get; set; } = new();
        public VideoFeedbackDto Feedback { get; set; } = new();

        [JsonPropertyName("report_path")]
        public string ReportPath { get; set; } = string.Empty;

        public DateTime UpdatedAt { get; set; }
    }
}


