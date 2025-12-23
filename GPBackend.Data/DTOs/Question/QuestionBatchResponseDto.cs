namespace GPBackend.DTOs.Question
{
    public class QuestionBatchResponseDto
    {
        public int TotalRequested { get; set; }
        public int SuccessfullyCreated { get; set; }
        public int Failed { get; set; }
        public List<QuestionResponseDto> CreatedQuestions { get; set; } = new List<QuestionResponseDto>();
        public List<string> Errors { get; set; } = new List<string>();
    }
}
