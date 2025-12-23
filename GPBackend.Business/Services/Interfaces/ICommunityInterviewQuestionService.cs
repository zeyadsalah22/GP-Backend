using GPBackend.DTOs.CommunityInterviewQuestion;
using GPBackend.DTOs.Common;

namespace GPBackend.Services.Interfaces
{
    public interface ICommunityInterviewQuestionService
    {
        Task<PagedResult<CommunityInterviewQuestionResponseDto>> GetFilteredQuestionsAsync(CommunityInterviewQuestionQueryDto queryDto, int? currentUserId);
        Task<CommunityInterviewQuestionDetailDto?> GetQuestionByIdAsync(int id, int? currentUserId);
        Task<CommunityInterviewQuestionResponseDto> CreateQuestionAsync(CommunityInterviewQuestionCreateDto createDto, int userId);
        Task<bool> MarkAskedThisTooAsync(int questionId, int userId);
        Task<bool> UnmarkAskedThisTooAsync(int questionId, int userId);
    }
}

