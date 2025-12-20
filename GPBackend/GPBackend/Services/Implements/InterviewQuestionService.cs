using AutoMapper;
using GPBackend.DTOs.InterviewQuestion;
using GPBackend.Repositories.Implements;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using GPBackend.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace GPBackend.Services.Implements
{
    public class InterviewQuestionService : IInterviewQuestionService
    {
        private readonly IInterviewQuestionRepository _interviewQuestionRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMLServiceClient _mlServiceClient;
        private readonly IMapper _mapper;
        private readonly ILogger<InterviewQuestionService> _logger;

        public InterviewQuestionService(IInterviewQuestionRepository interviewQuestionRepository,
                                 IApplicationRepository applicationRepository,
                                 IMLServiceClient mlServiceClient,
                                 IMapper mapper,
                                 ILogger<InterviewQuestionService> logger)
        {
            _interviewQuestionRepository = interviewQuestionRepository;
            _applicationRepository = applicationRepository;
            _mlServiceClient = mlServiceClient;
            _mapper = mapper;
            _logger = logger;
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

        public async Task<List<InterviewQuestionResponseDto>> GetInterviewQuestionsFromModelAsync(int applicationId, string jobDescription, string jobTitle)
        {
            try
            {
                string descriptionToUse = jobDescription;
                int numQuestions = 3;

                // If applicationId is provided, try to get the application details
                if (applicationId > 0)
                {
                    var application = await _applicationRepository.GetByIdAsync(applicationId);
                    if (application != null && !string.IsNullOrWhiteSpace(application.Description))
                    {
                        descriptionToUse = application.Description;
                        _logger.LogInformation("Using job description from application {ApplicationId}", applicationId);
                    }
                }

                // Validate that we have a job description
                if (string.IsNullOrWhiteSpace(descriptionToUse))
                {
                    throw new ArgumentException("Job description cannot be null or empty.");
                }

                _logger.LogInformation("Calling ML service to generate {NumQuestions} questions for job description (length: {Length})",
                    numQuestions, descriptionToUse.Length);

                // Call ML service to generate questions
                var questions = await _mlServiceClient.GenerateQuestionsAsync(descriptionToUse, numQuestions);

                if (questions == null || questions.Count == 0)
                {
                    _logger.LogWarning("ML service returned no questions");
                    throw new InvalidOperationException("No questions were returned from the ML service.");
                }

                // Map each string question to InterviewQuestionResponseDto
                var questionDtos = questions
                    .Select(q => new InterviewQuestionResponseDto { Question = q })
                    .ToList();

                _logger.LogInformation("Successfully generated {Count} questions from ML service", questionDtos.Count);

                return questionDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating interview questions from ML service");
                throw;
            }
        }
    }
}