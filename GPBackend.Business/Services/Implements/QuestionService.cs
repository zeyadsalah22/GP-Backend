using GPBackend.DTOs.Question;
using GPBackend.DTOs.Common;
using GPBackend.Services.Interfaces;
using AutoMapper;
using GPBackend.Repositories.Interfaces;
using GPBackend.Models;


namespace GPBackend.Services.Implements
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public QuestionService(
            IQuestionRepository QuestionRepository,
            IApplicationRepository applicationRepository,
            IMapper mapper
            )
        {
            _questionRepository = QuestionRepository;
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuestionResponseDto>> GetAllQuestion(int userId)
        {
            var question = await _questionRepository.GetAllQuestionAsync(userId);

            var questionDtos = _mapper.Map<IEnumerable<QuestionResponseDto>>(question);

            return questionDtos;
        }

        public async Task<PagedResult<QuestionResponseDto>> GetFilteredQuestionBasedOnQuery(int userId, QuestionQueryDto questionQueryDto)
        {
            var question = await _questionRepository.GetFilteredQuestionAsync(userId, questionQueryDto);

            var questionDto = _mapper.Map<List<QuestionResponseDto>>(question.Items);

            return new PagedResult<QuestionResponseDto>
            {
                Items = questionDto,
                TotalCount = question.TotalCount,
                PageSize = question.PageSize,
                PageNumber = question.PageNumber
            };
        }

        public async Task<QuestionResponseDto?> GetQuestionById(int userId, int questionId)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new GPBackend.Exceptions.NotFoundException($"Question with Id {questionId} not found");
            }
            if (question.Application == null || question.Application.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to access this question");
            }

            var questionDto = _mapper.Map<QuestionResponseDto>(question);

            return questionDto;
        }
        public async Task<QuestionResponseDto?> CreateNewQuestion(int userId, QuestionCreateDto questionCreateDto)
        {

            var application = await _applicationRepository.GetByIdAsync(questionCreateDto.ApplicationId);

            if (application == null)
            {
                throw new GPBackend.Exceptions.NotFoundException($"Application with Id {questionCreateDto.ApplicationId} not found");
            }
            if (application.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to create a question for this application");
            }

            var question = _mapper.Map<Question>(questionCreateDto);

            question.ApplicationId = questionCreateDto.ApplicationId;
            question.CreatedAt = DateTime.UtcNow;
            question.UpdatedAt = DateTime.UtcNow;

            question = await _questionRepository.CreateNewQuestionAsync(question);
            if (question == null)
            {
                throw new GPBackend.Exceptions.BadRequestException("Failed to create question");
            }

            var questionDto = _mapper.Map<QuestionResponseDto>(question);

            return questionDto;
        }
        public async Task<bool> UpdateQuestionById(int questionId, int userId, QuestionUpdateDto questionUpdateDto)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(questionId);

            if (question == null)
            {
                throw new GPBackend.Exceptions.NotFoundException($"Question with Id {questionId} not found");
            }

            var application = await _applicationRepository.GetByIdAsync(question.ApplicationId);

            if (application == null)
            {
                throw new GPBackend.Exceptions.NotFoundException($"Application with Id {question.ApplicationId} not found");
            }
            if (application.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to update this question");
            }

            _mapper.Map(questionUpdateDto, question);

            question.UpdatedAt = DateTime.UtcNow;

            return await _questionRepository.UpdateQuestionAsync(question);
        }
        public async Task<bool> DeleteQuestionById(int questionId, int userId)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(questionId);

            if (question == null)
            {
                throw new GPBackend.Exceptions.NotFoundException($"Question with Id {questionId} not found");
            }

            var application = await _applicationRepository.GetByIdAsync(question.ApplicationId);

            if (application == null)
            {
                throw new GPBackend.Exceptions.NotFoundException($"Application with Id {question.ApplicationId} not found");
            }
            if (application.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this question");
            }

            return await _questionRepository.DeleteQuestionByIdAsync(questionId);
        }

        public async Task<int> BulkDeleteQuestionsAsync(IEnumerable<int> ids, int userId)
        {
            return await _questionRepository.BulkSoftDeleteAsync(ids, userId);
        }

        public async Task<QuestionBatchResponseDto> CreateQuestionsBatchAsync(int userId, QuestionBatchCreateDto batchCreateDto)
        {
            var response = new QuestionBatchResponseDto
            {
                TotalRequested = batchCreateDto.Questions.Count,
                CreatedQuestions = new List<QuestionResponseDto>(),
                Errors = new List<string>()
            };

            // Group questions by application ID for batch validation
            var questionsByAppId = batchCreateDto.Questions
                .GroupBy(q => q.ApplicationId)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Validate all applications belong to the user
            var applicationIds = questionsByAppId.Keys;
            var applications = new Dictionary<int, Application>();

            foreach (var appId in applicationIds)
            {
                var application = await _applicationRepository.GetByIdAsync(appId);
                if (application == null)
                {
                    response.Errors.Add($"Application with ID {appId} not found");
                    continue;
                }
                if (application.UserId != userId)
                {
                    response.Errors.Add($"Application with ID {appId} does not belong to the user");
                    continue;
                }
                applications[appId] = application;
            }

            // Process questions for valid applications
            foreach (var kvp in questionsByAppId)
            {
                var appId = kvp.Key;
                var questionsForApp = kvp.Value;

                if (!applications.ContainsKey(appId))
                {
                    // Skip questions for invalid applications
                    response.Failed += questionsForApp.Count;
                    continue;
                }

                foreach (var questionCreateDto in questionsForApp)
                {
                    try
                    {
                        var question = _mapper.Map<Question>(questionCreateDto);
                        question.ApplicationId = appId;
                        question.CreatedAt = DateTime.UtcNow;
                        question.UpdatedAt = DateTime.UtcNow;

                        var createdQuestion = await _questionRepository.CreateNewQuestionAsync(question);
                        if (createdQuestion != null)
                        {
                            var questionDto = _mapper.Map<QuestionResponseDto>(createdQuestion);
                            response.CreatedQuestions.Add(questionDto);
                            response.SuccessfullyCreated++;
                        }
                        else
                        {
                            response.Failed++;
                            response.Errors.Add($"Failed to create question: {questionCreateDto.Question1}");
                        }
                    }
                    catch (Exception ex)
                    {
                        response.Failed++;
                        response.Errors.Add($"Error creating question '{questionCreateDto.Question1}': {ex.Message}");
                    }
                }
            }

            return response;
        }
    }
}