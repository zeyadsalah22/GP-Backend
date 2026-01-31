using AutoMapper;
using GPBackend.DTOs.CommunityInterviewQuestion;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.InterviewAnswer;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class CommunityInterviewQuestionService : ICommunityInterviewQuestionService
    {
        private readonly ICommunityInterviewQuestionRepository _questionRepository;
        private readonly IQuestionAskedByRepository _questionAskedByRepository;
        private readonly IInterviewAnswerHelpfulRepository _helpfulRepository;
        private readonly IMapper _mapper;
        private readonly ICommunityNotificationService _notificationService;

        public CommunityInterviewQuestionService(
            ICommunityInterviewQuestionRepository questionRepository,
            IQuestionAskedByRepository questionAskedByRepository,
            IInterviewAnswerHelpfulRepository helpfulRepository,
            IMapper mapper,
            ICommunityNotificationService notificationService)
        {
            _questionRepository = questionRepository;
            _questionAskedByRepository = questionAskedByRepository;
            _helpfulRepository = helpfulRepository;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<PagedResult<CommunityInterviewQuestionResponseDto>> GetFilteredQuestionsAsync(
            CommunityInterviewQuestionQueryDto queryDto, int? currentUserId)
        {
            var result = await _questionRepository.GetFilteredAsync(queryDto);

            var dtos = result.Items.Select(q => MapToResponseDto(q)).ToList();

            return new PagedResult<CommunityInterviewQuestionResponseDto>
            {
                Items = dtos,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };
        }

        public async Task<CommunityInterviewQuestionDetailDto?> GetQuestionByIdAsync(int id, int? currentUserId)
        {
            var question = await _questionRepository.GetByIdWithDetailsAsync(id);
            if (question == null) return null;

            var dto = new CommunityInterviewQuestionDetailDto
            {
                QuestionId = question.QuestionId,
                UserId = question.UserId,
                UserName = $"{question.User.Fname} {question.User.Lname}",
                UserProfilePictureUrl = question.User.ProfilePictureUrl,
                QuestionText = question.QuestionText,
                CompanyId = question.CompanyId,
                CompanyName = question.Company?.Name ?? question.CompanyName,
                CompanyLogo = question.Company?.LogoUrl ?? question.CompanyLogo,
                RoleType = question.RoleType,
                AddedRoleType = question.AddedRoleType,
                QuestionType = question.QuestionType,
                AddedQuestionType = question.AddedQuestionType,
                Difficulty = question.Difficulty,
                AskedCount = question.AskedCount,
                AnswerCount = question.AnswerCount,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
                CurrentUserAskedThis = false,
                Answers = question.Answers
                    .OrderByDescending(a => a.HelpfulCount)
                    .ThenByDescending(a => a.CreatedAt)
                    .Select(a => MapToAnswerResponseDto(a, currentUserId))
                    .ToList()
            };

            if (currentUserId.HasValue)
            {
                dto.CurrentUserAskedThis = await _questionAskedByRepository.ExistsAsync(id, currentUserId.Value);
            }

            return dto;
        }

        public async Task<CommunityInterviewQuestionResponseDto> CreateQuestionAsync(
            CommunityInterviewQuestionCreateDto createDto, int userId)
        {
            var question = _mapper.Map<CommunityInterviewQuestion>(createDto);
            question.UserId = userId;

            // If company name is provided but no company logo, generate it using Clearbit API
            if (!string.IsNullOrWhiteSpace(question.CompanyName) && string.IsNullOrWhiteSpace(question.CompanyLogo))
            {
                question.CompanyLogo = $"https://logo.clearbit.com/{question.CompanyName.ToLower()}.com";
            }

            var createdQuestion = await _questionRepository.CreateAsync(question);

            // Reload with navigation properties
            var questionWithDetails = await _questionRepository.GetByIdAsync(createdQuestion.QuestionId);

            return MapToResponseDto(questionWithDetails!);
        }

        public async Task<bool> MarkAskedThisTooAsync(int questionId, int userId)
        {
            // Check if already marked
            if (await _questionAskedByRepository.ExistsAsync(questionId, userId))
            {
                return false;
            }

            var questionAskedBy = new QuestionAskedBy
            {
                QuestionId = questionId,
                UserId = userId
            };

            await _questionAskedByRepository.CreateAsync(questionAskedBy);
            await _questionRepository.IncrementAskedCountAsync(questionId);
            
            // Notify question owner
            await _notificationService.NotifyQuestionAskedThisTooAsync(questionId, userId);

            return true;
        }

        public async Task<bool> UnmarkAskedThisTooAsync(int questionId, int userId)
        {
            // Check if not marked
            if (!await _questionAskedByRepository.ExistsAsync(questionId, userId))
            {
                return false;
            }

            await _questionAskedByRepository.DeleteAsync(questionId, userId);
            await _questionRepository.DecrementAskedCountAsync(questionId);

            return true;
        }

        private CommunityInterviewQuestionResponseDto MapToResponseDto(CommunityInterviewQuestion question)
        {
            return new CommunityInterviewQuestionResponseDto
            {
                QuestionId = question.QuestionId,
                UserId = question.UserId,
                UserName = $"{question.User.Fname} {question.User.Lname}",
                QuestionText = question.QuestionText,
                CompanyId = question.CompanyId,
                CompanyName = question.Company?.Name ?? question.CompanyName,
                CompanyLogo = question.Company?.LogoUrl ?? question.CompanyLogo,
                RoleType = question.RoleType,
                AddedRoleType = question.AddedRoleType,
                QuestionType = question.QuestionType,
                AddedQuestionType = question.AddedQuestionType,
                Difficulty = question.Difficulty,
                AskedCount = question.AskedCount,
                AnswerCount = question.AnswerCount,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt
            };
        }

        private InterviewAnswerResponseDto MapToAnswerResponseDto(InterviewAnswer answer, int? currentUserId)
        {
            return new InterviewAnswerResponseDto
            {
                AnswerId = answer.AnswerId,
                QuestionId = answer.QuestionId,
                UserId = answer.UserId,
                UserName = $"{answer.User.Fname} {answer.User.Lname}",
                UserProfilePictureUrl = answer.User.ProfilePictureUrl,
                AnswerText = answer.AnswerText,
                GotOffer = answer.GotOffer,
                HelpfulCount = answer.HelpfulCount,
                CreatedAt = answer.CreatedAt,
                UpdatedAt = answer.UpdatedAt,
                CurrentUserMarkedHelpful = currentUserId.HasValue &&
                    answer.HelpfulVotes.Any(v => v.UserId == currentUserId.Value)
            };
        }
    }
}

