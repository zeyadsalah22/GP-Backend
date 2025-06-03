using GPBackend.DTOs.Common;
using GPBackend.DTOs.Interview;

namespace GPBackend.Services.Interfaces
{
    public interface IInterviewService
    {
        /// <summary>
        /// Retrieves a list of mock interview questions based on the job description and title.
        /// </summary>
        /// <param name="jobDescription">The job description to filter questions.</param>
        /// <param name="jobTitle">The job title to filter questions.</param>
        /// <returns>A list of mock interview questions.</returns>
        Task<IEnumerable<InterviewResponseDto>> GetAllInterviewsByUserIdAsync(int userId);
        Task<PagedResult<InterviewResponseDto>> GetFilteredInterviewsAsync(int userId, InterviewQueryDto interviewQueryDto);
        Task<InterviewResponseDto?> GetInterviewByIdAsync(int userId, int interviewId);
        Task<InterviewResponseDto?> CreateInterviewAsync(int userId, InterviewCreateDto interviewCreateDto);
        Task<bool> UpdateInterviewByIdAsync(int userId, int interviewId, InterviewUpdateDto interviewUpdateDto);
        Task<bool> DeleteInterviewByIdAsync(int userId, int interviewId);


    }
}