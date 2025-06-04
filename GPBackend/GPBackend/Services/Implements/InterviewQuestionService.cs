using AutoMapper;
using GPBackend.DTOs.InterviewQuestion;
using GPBackend.Repositories.Implements;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using GPBackend.Models;
using System.Text.Json;

namespace GPBackend.Services.Implements
{
    public class InterviewQuestionService : IInterviewQuestionService
    {
        private readonly IInterviewQuestionRepository _interviewQuestionRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;
        private readonly string ModelApiURL = "http://localhost:8000/generate-questions";

        public InterviewQuestionService(IInterviewQuestionRepository interviewQuestionRepository,
                                 IApplicationRepository applicationRepository,
                                 IMapper mapper)
        {
            _interviewQuestionRepository = interviewQuestionRepository;
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<List<InterviewQuestionResponseDto>> CreateInterviewQuestionsAsync(List<InterviewQuestionCreateDto> interviewQuestionCreateDto)
        {
            List<InterviewQuestion> interviewQuestions = _mapper.Map<List<InterviewQuestion>>(interviewQuestionCreateDto);

            var createdQuestions = await _interviewQuestionRepository.CreateAsync(interviewQuestions);

            return _mapper.Map<List<InterviewQuestionResponseDto>>(createdQuestions);
        }

        public async Task<List<InterviewQuestionResponseDto>> UpdateInterviewQuestionAsync(List<InterviewQuestionUpdateDto> interviewQuestionUpdateDto)
        {
            var interviewQuestions = _mapper.Map<List<InterviewQuestion>>(interviewQuestionUpdateDto);
            foreach (var question in interviewQuestions)
            {
                question.UpdatedAt = DateTime.UtcNow;
            }
            var updatedQuestions = await _interviewQuestionRepository.UpdateAsync(interviewQuestions);
            return _mapper.Map<List<InterviewQuestionResponseDto>>(updatedQuestions);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _interviewQuestionRepository.DeleteAsync(id);
        }

        public async Task<InterviewQuestionResponseDto> GetByIdAsync(int id)
        {
            var interviewQuestion = await _interviewQuestionRepository.GetByIdAsync(id);
            return _mapper.Map<InterviewQuestionResponseDto>(interviewQuestion);
        }

        public async Task<List<InterviewQuestionResponseDto>> GetAllByInterviewIdAsync(int interviewId)
        {
            var interviewQuestions = await _interviewQuestionRepository.GetAllAsync(interviewId);
            return _mapper.Map<List<InterviewQuestionResponseDto>>(interviewQuestions);
        }

        public async Task<List<InterviewQuestionResponseDto>> GetInterviewQuestionsFromModelAsync(int applicationId, string jobDescription, string jobtitle)
        {
            // fetch questions from the model API
            var client = new HttpClient();

            var application = await _applicationRepository.GetByIdAsync(applicationId);

            HttpResponseMessage? response = null;

            if (application == null)
            {
                if (jobDescription == null || jobtitle == null)
                {
                    throw new ArgumentException("Job description and title cannot be null when application is not found.");
                }
                response = await client.PostAsJsonAsync(ModelApiURL, new
                {
                    description = jobDescription,
                    num_questions = 3,
                });
            }
            else
            {
                if (application.Description == null || application.JobTitle == null)
                {
                    throw new ArgumentException("Job description and title cannot be null when application is found.");
                }
                response = await client.PostAsJsonAsync(ModelApiURL, new
                {
                    description = application.Description,
                    num_questions = 3,
                });
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch interview questions from the model API.");
            }

            // var raw = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(raw);

            var questions = await response.Content.ReadFromJsonAsync<InterviewQuestionAIDto>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (questions == null || questions.Questions == null || questions.Questions.Count == 0)
            {
                throw new Exception("No questions were returned from the model API.");
            }

            // Manually map each string to InterviewQuestionResponseDto
            var questionDtos = questions.Questions
                .Select(q => new InterviewQuestionResponseDto { Question = q })
                .ToList();


            return questionDtos;

        }
    }
}