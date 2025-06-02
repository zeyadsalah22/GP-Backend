using GPBackend.DTOs.Interview;
using GPBackend.DTOs.Common;
using GPBackend.Models;


namespace GPBackend.Repositories.Interfaces
{
    public interface IInterviewRepository
    {
        /// <summary>
        /// Retrieves a list of mock interview questions based on the job description and title.
        /// </summary>
        /// <param name="jobDescription">The job description to filter questions.</param>
        /// <param name="jobTitle">The job title to filter questions.</param>
        /// <returns>A list of mock interview questions.</returns>
        // Task<IEnumerable<Interview>> GetInterviewQuestionsAsync(string jobDescription, string jobTitle);

        Task<IEnumerable<Interview>> GetAllInterviewsAsync(int userId);
        Task<PagedResult<Interview>> GetFilteredInterviewsAsync(int userId, InterviewQueryDto interviewQueryDto);
        Task<Interview?> GetInterviewByIdAsync(int userId, int interviewId);
        Task<int> CreateInterviewAsync(Interview interview);
        Task<bool> UpdateInterviewAsync(Interview interview);
        Task<bool> DeleteInterviewByIdAsync(int interviewId);
    }
}