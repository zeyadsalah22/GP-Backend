using GPBackend.DTOs.InterviewAnswer;

namespace GPBackend.Services.Interfaces
{
    public interface IInterviewAnswerService
    {
        Task<InterviewAnswerResponseDto> CreateAnswerAsync(int questionId, InterviewAnswerCreateDto createDto, int userId);
        Task<bool> MarkAnswerAsHelpfulAsync(int answerId, int userId);
        Task<bool> UnmarkAnswerAsHelpfulAsync(int answerId, int userId);
    }
}

