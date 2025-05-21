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

        public async Task<QuestionResponseDto?> GetQuestionById(int questionId, int applicationId)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(questionId);

            if (question == null || question.ApplicationId != applicationId)
            {
                return null;
            }

            var questionDto = _mapper.Map<QuestionResponseDto>(question);

            return questionDto;
        }
        public async Task<QuestionResponseDto?> CreateNewQuestion(int userId, QuestionCreateDto questionCreateDto)
        {

            var application = await _applicationRepository.GetByIdAsync(questionCreateDto.ApplicationId);

            if (application == null || application.UserId != userId)
            {
                return null;
            }

            var question = _mapper.Map<Question>(questionCreateDto);

            question.ApplicationId = questionCreateDto.ApplicationId;
            question.CreatedAt = DateTime.UtcNow;
            question.UpdatedAt = DateTime.UtcNow;

            question = await _questionRepository.CreateNewQuestionAsync(question);
            if (question == null)
            {
                throw new Exception("An error ocurred while creating the question");
            }

            var questionDto = _mapper.Map<QuestionResponseDto>(question);

            return questionDto;
        }
        public async Task<bool> UpdateQuestionById(int questionId, int userId, QuestionUpdateDto questionUpdateDto)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(questionId);

            if (question == null)
            {
                return false;
            }

            var application = await _applicationRepository.GetByIdAsync(question.ApplicationId);

            if (application == null || application.UserId != userId)
            {
                return false;
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
                return false;
            }

            var application = await _applicationRepository.GetByIdAsync(question.ApplicationId);

            if (application == null || application.UserId != userId)
            {
                return false;
            }

            return await _questionRepository.DeleteQuestionByIdAsync(questionId);
        }
    }
}