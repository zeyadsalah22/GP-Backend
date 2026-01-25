using System.Text.Json.Serialization;

namespace GPBackend.DTOs.InterviewFeedback
{
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
}


