using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace GPBackend.DTOs.InterviewFeedback
{
    public class InterviewFeedbackHealthResponseDto
    {
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("testing_mode")]
        public bool TestingMode { get; set; }

        [JsonPropertyName("answers_model_loaded")]
        public bool AnswersModelLoaded { get; set; }

        [JsonPropertyName("video_model_available")]
        public bool VideoModelAvailable { get; set; }
    }

    public class GradeAnswerRequestDto
    {
        [Required]
        [JsonPropertyName("question")]
        public string Question { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("answer")]
        public string Answer { get; set; } = string.Empty;

        [JsonPropertyName("context")]
        public string? Context { get; set; }
    }

    public class GradeAnswerResponseDto
    {
        public double Score { get; set; }
        public string Feedback { get; set; } = string.Empty;
        public List<string> Strengths { get; set; } = new();
        public List<string> Improvements { get; set; } = new();
    }

    public class GradeAnswersBatchRequestDto
    {
        [Required]
        [JsonPropertyName("items")]
        public List<GradeAnswersBatchItemDto> Items { get; set; } = new();

        [JsonPropertyName("context")]
        public string? Context { get; set; }
    }

    public class GradeAnswersBatchItemDto
    {
        [Required]
        [JsonPropertyName("question")]
        public string Question { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("answer")]
        public string Answer { get; set; } = string.Empty;
    }

    public class GradeAnswersBatchResponseDto
    {
        public List<GradeAnswersBatchResultDto> Results { get; set; } = new();

        [JsonPropertyName("total_graded")]
        public int TotalGraded { get; set; }
    }

    public class GradeAnswersBatchResultDto
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public double Score { get; set; }
        public string Feedback { get; set; } = string.Empty;
    }

    public class AnalyzeVideoRequestDto
    {
        [Required]
        public IFormFile File { get; set; } = null!;
    }

    public class AnalyzeVideoResponseDto
    {
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("video_path")]
        public string VideoPath { get; set; } = string.Empty;

        public VideoMetricsDto Metrics { get; set; } = new();
        public VideoFeedbackDto Feedback { get; set; } = new();

        [JsonPropertyName("report_path")]
        public string ReportPath { get; set; } = string.Empty;
    }

    public class VideoMetricsDto
    {
        public double Confidence { get; set; }
        public double Engagement { get; set; }
        public double Stress { get; set; }
        public double Authenticity { get; set; }
    }

    public class VideoFeedbackDto
    {
        public string Summary { get; set; } = string.Empty;
        public List<VideoFeedbackItemDto> Strengths { get; set; } = new();
        public List<VideoFeedbackItemDto> Improvements { get; set; } = new();
        public List<VideoRecommendationDto> Recommendations { get; set; } = new();
    }

    public class VideoFeedbackItemDto
    {
        public string Metric { get; set; } = string.Empty;
        public double Score { get; set; }
        public double Threshold { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        // Optional fields that may appear depending on the metric/model
        [JsonPropertyName("variety_message")]
        public string? VarietyMessage { get; set; }

        [JsonPropertyName("trend_message")]
        public string? TrendMessage { get; set; }

        [JsonPropertyName("peaks_message")]
        public string? PeaksMessage { get; set; }

        [JsonPropertyName("num_peaks")]
        public int? NumPeaks { get; set; }

        [JsonPropertyName("eye_message")]
        public string? EyeMessage { get; set; }

        [JsonPropertyName("duration_message")]
        public string? DurationMessage { get; set; }

        [JsonPropertyName("num_smiles")]
        public int? NumSmiles { get; set; }
    }

    public class VideoRecommendationDto
    {
        public string Category { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
    }
}


