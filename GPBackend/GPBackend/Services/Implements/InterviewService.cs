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
        private readonly IInterviewRepository _interviewRepository;
        private readonly IInterviewQuestionService _interviewQuestionService;
        private readonly IMapper _mapper;

        public InterviewService(IInterviewRepository interviewRepository,
                                IInterviewQuestionService interviewQuestionService,
                                IMapper mapper)
        {
            _interviewRepository = interviewRepository;
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

            // map from InterviewCreateDto to Interview
            var interviewToCreate = _mapper.Map<Interview>(interviewCreateDto);

            // check if the applicationId is provided
            if (interviewCreateDto.ApplicationId == null)
            {
                if (interviewCreateDto.CompanyId == null || string.IsNullOrEmpty(interviewCreateDto.JobDescription) || string.IsNullOrEmpty(interviewCreateDto.Position))
                {
                    return null; // Job description and position are required if applicationId is not provided
                }
            }

            // create the interview
            var createdInterviewId = await _interviewRepository.CreateInterviewAsync(interviewToCreate);


            // start recording, return Questions ==> model
            var AIModelQuestions = await _interviewQuestionService.GetInterviewQuestionsFromModelAsync(interviewCreateDto.ApplicationId ?? 0,
                                                                                                    interviewCreateDto.JobDescription,
                                                                                                    interviewCreateDto.Position);

            // map from AIModelQuestions to InterviewQuestionCreateDto
            var interviewQuestionsToCreate = _mapper.Map<List<InterviewQuestionCreateDto>>(AIModelQuestions);


            // set the InterviewId for each question
            interviewQuestionsToCreate.ForEach(q => q.InterviewId = createdInterviewId);

            // tranform questions to interviewQuestions
            var createdInterviewQuestions = await _interviewQuestionService.CreateInterviewQuestionsAsync(interviewQuestionsToCreate);

            var createdInterview = await _interviewRepository.GetInterviewByIdAsync(userId, createdInterviewId);
            
            if(createdInterview == null || createdInterview.UserId != userId || createdInterview.InterviewId != createdInterviewId)
            {
                return null; // Failed to retrieve the created interview
            }

            createdInterview.InterviewQuestions = _mapper.Map<ICollection<InterviewQuestion>>(createdInterviewQuestions);

            // map from createdInterview to InterviewResponseDto
            var interviewResponseDto = _mapper.Map<InterviewResponseDto>(createdInterview);

            return interviewResponseDto;
        }

        public async Task<bool> UpdateInterviewByIdAsync(int userId, int interviewId, InterviewUpdateDto updateInterviewDto)
        {
            var interview = await _interviewRepository.GetInterviewByIdAsync(userId, interviewId);
            if (interview == null || interview.UserId != userId)
            {
                return false; // Interview not found or does not belong to the user
            }

            // Map the update DTO to the interview entity
            _mapper.Map(updateInterviewDto, interview);
            interview.InterviewId = interviewId; // Ensure the ID is set for the update
                                                 // interview.UserId = userId; // Ensure the user ID is set for the update
                                                 // Update the interview in the repository
            
            var interviewQuestions = _mapper.Map<List<InterviewQuestion>>(updateInterviewDto.InterviewQuestions);

            interview.InterviewQuestions = interviewQuestions;

            var updateResult = await _interviewRepository.UpdateInterviewAsync(interview);
            if (!updateResult)
            {
                return false; // Update failed
            }

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
