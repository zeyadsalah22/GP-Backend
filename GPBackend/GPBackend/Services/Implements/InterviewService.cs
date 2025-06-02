using AutoMapper;
using GPBackend.DTOs.Interview;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.InterviewQuestion;
using GPBackend.Services.Interfaces;
using GPBackend.Repositories.Interfaces;

using GPBackend.Models;


namespace GPBackend.Services.Implements
{
    public class InterviewService : IInterviewService
    {
        private readonly HttpClient _httpClient;

        private readonly IInterviewRepository _interviewRepository;
        private readonly IInterviewQuestionService _interviewQuestionService;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public InterviewService(HttpClient httpClient,
                                IInterviewRepository interviewRepository,
                                IInterviewQuestionService interviewQuestionService,
                                IApplicationRepository applicationRepository,
                                IMapper mapper)
        {
            _httpClient = httpClient;
            _interviewRepository = interviewRepository;
            _applicationRepository = applicationRepository;
            _interviewQuestionService = interviewQuestionService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InterviewResponseDto>> GetAllInterviewsByUserIdAsync(int userId)
        {
            var result = await _interviewRepository.GetAllInterviewsAsync(userId);
            var interviewDtos = _mapper.Map<IEnumerable<InterviewResponseDto>>(result);

            return interviewDtos;
        }

        public async Task<PagedResult<InterviewResponseDto>> GetFilteredInterviewsAsync(int userId, InterviewQueryDto interviewQueryDto)
        {
            // Get filtered interviews from the repository
            var pagedResult = await _interviewRepository.GetFilteredInterviewsAsync(userId, interviewQueryDto);

            // Map the result to DTOs
            var interviewDtos = _mapper.Map<List<InterviewResponseDto>>(pagedResult.Items);

            // Return the paged result with mapped items
            return new PagedResult<InterviewResponseDto>
            {
                Items = interviewDtos,
                TotalCount = pagedResult.TotalCount,
                PageSize = pagedResult.PageSize,
                PageNumber = pagedResult.PageNumber
            };
        }


        public async Task<InterviewResponseDto?> GetInterviewByIdAsync(int userId, int interviewId)
        {
            // Check if the interview exists
            var interview = await _interviewRepository.GetInterviewByIdAsync(userId, interviewId);
            if (interview == null || interview.UserId != userId)
            {
                return null; // Interview not found or does not belong to the user
            }

            // Map the interview to the response DTO
            return _mapper.Map<InterviewResponseDto>(interview);
        }

        public async Task<InterviewResponseDto?> CreateInterviewAsync(int userId, InterviewCreateDto interviewCreateDto)
        {

            if (interviewCreateDto.UserId != userId)
            {
                return null;
            }
            // start recording, return Questions ==> model
            var AIModelQuestions = await _interviewQuestionService.GetInterviewQuestionsFromModelAsync(interviewCreateDto.ApplicationId,
                                                                                                    interviewCreateDto.JobDescription,
                                                                                                    interviewCreateDto.Position);

            // map from AIModelQuestions to InterviewQuestionCreateDto
            var interviewQuestionsToCreate = _mapper.Map<List<InterviewQuestionCreateDto>>(AIModelQuestions);

            // tranform questions to interviewQuestions
            var createdInterviewQuestions = await _interviewQuestionService.CreateInterviewQuestionsAsync(interviewQuestionsToCreate);

            // map from InterviewCreateDto to Interview
            var interviewToCreate = _mapper.Map<Interview>(interviewCreateDto);
            
            interviewToCreate.InterviewQuestions = (ICollection<InterviewQuestion>)createdInterviewQuestions;

            // create the interview
            var createdInterview = await _interviewRepository.CreateInterviewAsync(interviewToCreate);

            // map from createdInterview to InterviewResponseDto
            var interviewResponseDto = _mapper.Map<InterviewResponseDto>(createdInterview);

            interviewResponseDto.InterviewQuestions = createdInterviewQuestions;

            return interviewResponseDto;
        }

        public async Task<bool> UpdateInterviewByIdAsync(int userId, int interviewId, InterviewUpdateDto updateInterviewDto)
        {
            var interview = await _interviewRepository.GetInterviewByIdAsync(userId, interviewId);
            if (interview == null || interview.UserId != userId)
            {
                return false; // Interview not found or does not belong to the user
            }

            var updatedQuestions = await _interviewQuestionService.UpdateInterviewQuestionAsync(updateInterviewDto.InterviewQuestions);
            if (updatedQuestions == null)
            {
                return false; // Failed to update interview questions
            }

            // Map the update DTO to the interview entity
            var interviewToUpdate = _mapper.Map<Interview>(updateInterviewDto);

            interviewToUpdate.InterviewQuestions = (ICollection<InterviewQuestion>)updatedQuestions;

            var updatedInterview = await _interviewRepository.UpdateInterviewAsync(interviewToUpdate);
            if (updatedInterview == false)
            {
                return false; // Failed to update interview
            }

            // Map the updated interview to the response DTO
            _mapper.Map<InterviewResponseDto>(updatedInterview);

            return true;
        }

        public async Task<bool> DeleteInterviewByIdAsync(int userId, int interviewId)
        {
            // Check if the interview exists
            var interview = await _interviewRepository.GetInterviewByIdAsync(userId, interviewId);
            if (interview == null || interview.UserId != userId)
            {
                return false; // Interview not found or does not belong to the user
            }

            // Delete the interview
            return await _interviewRepository.DeleteInterviewByIdAsync(interviewId);
        }
    }
}
