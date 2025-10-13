using GPBackend.DTOs.Common;
using GPBackend.DTOs.WeeklyGoal;

namespace GPBackend.Services.Interfaces
{
    public interface IWeeklyGoalService
    {
        Task<WeeklyGoalResponseDto?> GetWeeklyGoalByIdAsync(int id, int userId);
        Task<PagedResult<WeeklyGoalResponseDto>> GetFilteredWeeklyGoalsAsync(int userId, WeeklyGoalQueryDto queryDto);
        Task<WeeklyGoalResponseDto?> GetCurrentWeekGoalAsync(int userId);
        Task<WeeklyGoalResponseDto> CreateWeeklyGoalAsync(int userId, WeeklyGoalCreateDto createDto);
        Task<bool> UpdateWeeklyGoalAsync(int id, int userId, WeeklyGoalUpdateDto updateDto);
        Task<bool> DeleteWeeklyGoalAsync(int id, int userId);
        Task<WeeklyGoalStatsDto> GetWeeklyGoalStatsAsync(int userId);
    }
}

