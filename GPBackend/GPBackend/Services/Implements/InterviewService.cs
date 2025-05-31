using GPBackend.DTOs.Interview;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class InterviewService : IInterviewService
    {
        private readonly HttpClient _httpClient;

        private readonly IInterviewRepository _interviewRepository;

        public InterviewService(HttpClient httpClient, IInterviewRepository interviewRepository)
        {
            _httpClient = httpClient;
            _interviewRepository = interviewRepository;
        }

        public async Task<PagedResult<InterviewResponseDto>> GetAllInterviewsAsync(int userId, InterviewQueryDto interviewQueryDto)
        {
            
        }


        public async Task<InterviewResponseDto> GetInterviewByIdAsync(int userId, int interviewId)
        {
            
        }

        public async Task<InterviewResponseDto> CreateInterviewAsync(int userId, CreateInterviewDto createInterviewDto)
        {
            
        }

        public async Task<InterviewResponseDto> UpdateInterviewByIdAsync(int userId, int interviewId, UpdateInterviewDto updateInterviewDto)
        {
            
        }

        public async Task<bool> DeleteInterviewByIdAsync(int userId, int interviewId)
        {
            // Check if the interview exists
            var interview = await _interviewRepository.GetInterviewByIdAsync(interviewId);
            if (interview == null || interview.UserId != userId)
            {
                return false; // Interview not found or does not belong to the user
            }

            // Delete the interview
            return await _interviewRepository.DeleteAsync(interview);
        }


        public async Task<IEnumerable<InterviewResponseDto>> GetInterviewQuestionsAsync(string jobDescription, string jobTitle)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:8000/generate-questions", new
            {
                job_description = jobDescription,
                job_title = jobTitle
            });
            // Check if the response is successful
            if (!response.IsSuccessStatusCode)
            {
                // Log the error or handle it as needed
                return Enumerable.Empty<InterviewResponseDto>();
            }
            // Deserialize the response content to a list of InterviewResponseDto

            // debug the output of the response
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response from API: {responseContent}");

            // var questions = await response.Content.ReadFromJsonAsync<IEnumerable<InterviewResponseDto>>();
            // return questions ?? Enumerable.Empty<InterviewResponseDto>();

            return null;
        }
    }
}
