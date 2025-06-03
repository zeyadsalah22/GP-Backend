using GPBackend.DTOs.InterviewQuestion;
using GPBackend.Models;

namespace GPBackend.Services.Interfaces
{
    public interface IInterviewQuestionService
    {
        Task<List<InterviewQuestionResponseDto>> CreateInterviewQuestionsAsync(List<InterviewQuestionCreateDto> interviewQuestionCreateDto);
        Task<List<InterviewQuestionResponseDto>> UpdateInterviewQuestionAsync(List<InterviewQuestionUpdateDto> interviewQuestionUpdateDto);
        Task<bool> DeleteAsync(int id);
        Task<InterviewQuestionResponseDto> GetByIdAsync(int id);
        Task<List<InterviewQuestionResponseDto>> GetAllByInterviewIdAsync(int interviewId);

        Task<List<InterviewQuestionResponseDto>> GetInterviewQuestionsFromModelAsync(int applicationId, string jobDescription, string jobtitle);
    }
}