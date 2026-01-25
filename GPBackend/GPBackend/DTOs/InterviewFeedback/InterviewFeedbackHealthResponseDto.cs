using System.Text.Json.Serialization;

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
}


