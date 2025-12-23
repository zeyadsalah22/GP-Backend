using AutoMapper;
using GPBackend.DTOs.InterviewAnswer;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class InterviewAnswerService : IInterviewAnswerService
    {
        private readonly IInterviewAnswerRepository _answerRepository;
        private readonly IInterviewAnswerHelpfulRepository _helpfulRepository;
        private readonly ICommunityInterviewQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public InterviewAnswerService(
            IInterviewAnswerRepository answerRepository,
            IInterviewAnswerHelpfulRepository helpfulRepository,
            ICommunityInterviewQuestionRepository questionRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _answerRepository = answerRepository;
            _helpfulRepository = helpfulRepository;
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<InterviewAnswerResponseDto> CreateAnswerAsync(
            int questionId, InterviewAnswerCreateDto createDto, int userId)
        {
            var answer = _mapper.Map<InterviewAnswer>(createDto);
            answer.QuestionId = questionId;
            answer.UserId = userId;

            var createdAnswer = await _answerRepository.CreateAsync(answer);

            await _questionRepository.IncrementAnswerCountAsync(questionId);

            // check it again -> to be delete it soon (maybe) 
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.ReputationPoints += 5;
                await _userRepository.UpdateAsync(user);
            }

            var answerWithDetails = await _answerRepository.GetByIdAsync(createdAnswer.AnswerId);

            return MapToResponseDto(answerWithDetails!, userId);
        }

        public async Task<bool> MarkAnswerAsHelpfulAsync(int answerId, int userId)
        {
            if (await _helpfulRepository.ExistsAsync(answerId, userId))
            {
                return false;
            }

            var answer = await _answerRepository.GetByIdAsync(answerId);
            if (answer == null) return false;

            var helpful = new InterviewAnswerHelpful
            {
                AnswerId = answerId,
                UserId = userId
            };

            await _helpfulRepository.CreateAsync(helpful);
            await _answerRepository.IncrementHelpfulCountAsync(answerId);

            // same here
            var answerAuthor = await _userRepository.GetByIdAsync(answer.UserId);
            if (answerAuthor != null)
            {
                answerAuthor.ReputationPoints += 15;
                await _userRepository.UpdateAsync(answerAuthor);
            }

            return true;
        }

        public async Task<bool> UnmarkAnswerAsHelpfulAsync(int answerId, int userId)
        {
            var helpful = await _helpfulRepository.GetByAnswerAndUserAsync(answerId, userId);
            if (helpful == null) return false;

            var answer = await _answerRepository.GetByIdAsync(answerId);
            if (answer == null) return false;

            await _helpfulRepository.DeleteAsync(helpful.Id);
            await _answerRepository.DecrementHelpfulCountAsync(answerId);

            // remove reputation
            var answerAuthor = await _userRepository.GetByIdAsync(answer.UserId);
            if (answerAuthor != null)
            {
                answerAuthor.ReputationPoints = Math.Max(0, answerAuthor.ReputationPoints - 15);
                await _userRepository.UpdateAsync(answerAuthor);
            }

            return true;
        }

        private InterviewAnswerResponseDto MapToResponseDto(InterviewAnswer answer, int? currentUserId)
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

