using GPBackend.DTOs.Common;
using GPBackend.DTOs.WeeklyGoal;
using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IWeeklyGoalRepository
    {
        Task<WeeklyGoal?> GetByIdAsync(int id);
        Task<WeeklyGoal?> GetByWeekStartDateAsync(int userId, DateOnly weekStartDate);
        Task<PagedResult<WeeklyGoal>> GetFilteredWeeklyGoalsAsync(int userId, WeeklyGoalQueryDto queryDto);
        Task<IEnumerable<WeeklyGoal>> GetAllByUserIdAsync(int userId);
        Task<int> CreateAsync(WeeklyGoal weeklyGoal);
        Task<bool> UpdateAsync(WeeklyGoal weeklyGoal);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> GetApplicationCountForWeekAsync(int userId, DateOnly weekStart, DateOnly weekEnd);
        Task<WeeklyGoal?> GetCurrentWeekGoalAsync(int userId);
    }
}

