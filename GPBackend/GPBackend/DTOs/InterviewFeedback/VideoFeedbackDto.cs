namespace GPBackend.DTOs.InterviewFeedback
{
    public class VideoFeedbackDto
    {
        public string Summary { get; set; } = string.Empty;
        public List<VideoFeedbackItemDto> Strengths { get; set; } = new();
        public List<VideoFeedbackItemDto> Improvements { get; set; } = new();
        public List<VideoRecommendationDto> Recommendations { get; set; } = new();
    }
}


