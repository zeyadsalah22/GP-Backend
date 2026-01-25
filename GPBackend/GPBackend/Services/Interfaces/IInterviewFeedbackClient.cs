using GPBackend.DTOs.InterviewFeedback;

namespace GPBackend.Services.Interfaces
{
    public interface IInterviewFeedbackClient
    {
        Task<InterviewFeedbackHealthResponseDto> HealthAsync(CancellationToken cancellationToken = default);
        Task<GradeAnswerResponseDto> GradeAnswerAsync(GradeAnswerRequestDto request, CancellationToken cancellationToken = default);
        Task<GradeAnswersBatchResponseDto> GradeAnswersBatchAsync(GradeAnswersBatchRequestDto request, CancellationToken cancellationToken = default);
        Task<AnalyzeVideoResponseDto> AnalyzeVideoAsync(
            Stream videoStream,
            string fileName,
            string? contentType,
            CancellationToken cancellationToken = default);
    }
}


