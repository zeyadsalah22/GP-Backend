using GPBackend.DTOs.InterviewFeedback;

namespace GPBackend.Services.Interfaces
{
    public interface IInterviewFeedbackService
    {
        Task<InterviewQuestionFeedbackResponseDto> GradeInterviewQuestionAsync(
            int userId,
            int interviewId,
            int interviewQuestionId,
            string? context,
            CancellationToken cancellationToken = default);

        Task<List<InterviewQuestionFeedbackResponseDto>> GradeInterviewQuestionsBatchAsync(
            int userId,
            int interviewId,
            List<int> interviewQuestionIds,
            string? context,
            CancellationToken cancellationToken = default);

        Task<InterviewVideoFeedbackResponseDto> AnalyzeInterviewVideoAsync(
            int userId,
            int interviewId,
            Stream videoStream,
            string fileName,
            string? contentType,
            CancellationToken cancellationToken = default);

        Task<List<InterviewQuestionFeedbackResponseDto>> GetAnswersFeedbackAsync(
            int userId,
            int interviewId,
            CancellationToken cancellationToken = default);

        Task<InterviewVideoFeedbackResponseDto?> GetVideoFeedbackAsync(
            int userId,
            int interviewId,
            CancellationToken cancellationToken = default);
    }
}


