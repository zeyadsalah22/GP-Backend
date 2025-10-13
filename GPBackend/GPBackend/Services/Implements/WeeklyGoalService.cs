using AutoMapper;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.WeeklyGoal;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class WeeklyGoalService : IWeeklyGoalService
    {
        private readonly IWeeklyGoalRepository _weeklyGoalRepository;
        private readonly IMapper _mapper;

        public WeeklyGoalService(
            IWeeklyGoalRepository weeklyGoalRepository,
            IMapper mapper)
        {
            _weeklyGoalRepository = weeklyGoalRepository;
            _mapper = mapper;
        }

        public async Task<WeeklyGoalResponseDto?> GetWeeklyGoalByIdAsync(int id, int userId)
        {
            var weeklyGoal = await _weeklyGoalRepository.GetByIdAsync(id);
            
            if (weeklyGoal == null || weeklyGoal.UserId != userId)
            {
                return null;
            }
            
            return await MapToResponseDtoAsync(weeklyGoal);
        }

        public async Task<WeeklyGoalResponseDto?> GetCurrentWeekGoalAsync(int userId)
        {
            var weeklyGoal = await _weeklyGoalRepository.GetCurrentWeekGoalAsync(userId);
            
            if (weeklyGoal == null)
            {
                return null;
            }
            
            return await MapToResponseDtoAsync(weeklyGoal);
        }

        public async Task<PagedResult<WeeklyGoalResponseDto>> GetFilteredWeeklyGoalsAsync(int userId, WeeklyGoalQueryDto queryDto)
        {
            var pagedResult = await _weeklyGoalRepository.GetFilteredWeeklyGoalsAsync(userId, queryDto);
            
            var responseDtos = new List<WeeklyGoalResponseDto>();
            foreach (var weeklyGoal in pagedResult.Items)
            {
                var dto = await MapToResponseDtoAsync(weeklyGoal);
                
                // Apply progress filters if specified
                if (queryDto.MinProgress.HasValue && dto.ProgressPercentage < queryDto.MinProgress.Value)
                    continue;
                if (queryDto.MaxProgress.HasValue && dto.ProgressPercentage > queryDto.MaxProgress.Value)
                    continue;
                if (queryDto.IsCompleted.HasValue && dto.IsCompleted != queryDto.IsCompleted.Value)
                    continue;
                    
                responseDtos.Add(dto);
            }
            
            return new PagedResult<WeeklyGoalResponseDto>
            {
                Items = responseDtos,
                TotalCount = pagedResult.TotalCount,
                PageSize = pagedResult.PageSize,
                PageNumber = pagedResult.PageNumber
            };
        }

        public async Task<WeeklyGoalResponseDto> CreateWeeklyGoalAsync(int userId, WeeklyGoalCreateDto createDto)
        {
            // Calculate week end date (6 days after start)
            var weekEndDate = createDto.WeekStartDate.AddDays(6);
            
            // Check if goal already exists for this week
            var existingGoal = await _weeklyGoalRepository.GetByWeekStartDateAsync(userId, createDto.WeekStartDate);
            if (existingGoal != null)
            {
                throw new InvalidOperationException("A goal already exists for this week");
            }
            
            var weeklyGoal = _mapper.Map<WeeklyGoal>(createDto);
            weeklyGoal.UserId = userId;
            weeklyGoal.WeekEndDate = weekEndDate;
            weeklyGoal.CreatedAt = DateTime.UtcNow;
            weeklyGoal.UpdatedAt = DateTime.UtcNow;
            weeklyGoal.IsDeleted = false;
            
            var id = await _weeklyGoalRepository.CreateAsync(weeklyGoal);
            
            var createdGoal = await _weeklyGoalRepository.GetByIdAsync(id);
            if (createdGoal == null)
            {
                throw new InvalidOperationException("Failed to retrieve created weekly goal");
            }
            
            return await MapToResponseDtoAsync(createdGoal);
        }

        public async Task<bool> UpdateWeeklyGoalAsync(int id, int userId, WeeklyGoalUpdateDto updateDto)
        {
            var weeklyGoal = await _weeklyGoalRepository.GetByIdAsync(id);
            if (weeklyGoal == null || weeklyGoal.UserId != userId)
            {
                return false;
            }
            
            _mapper.Map(updateDto, weeklyGoal);
            weeklyGoal.UpdatedAt = DateTime.UtcNow;
            
            return await _weeklyGoalRepository.UpdateAsync(weeklyGoal);
        }

        public async Task<bool> DeleteWeeklyGoalAsync(int id, int userId)
        {
            var weeklyGoal = await _weeklyGoalRepository.GetByIdAsync(id);
            if (weeklyGoal == null || weeklyGoal.UserId != userId)
            {
                return false;
            }
            
            return await _weeklyGoalRepository.DeleteAsync(id);
        }

        public async Task<WeeklyGoalStatsDto> GetWeeklyGoalStatsAsync(int userId)
        {
            var allGoals = await _weeklyGoalRepository.GetAllByUserIdAsync(userId);
            var goalsList = allGoals.ToList();
            
            var stats = new WeeklyGoalStatsDto
            {
                TotalGoals = goalsList.Count,
                CompletedGoals = 0,
                ActiveGoals = 0,
                AverageCompletionRate = 0
            };
            
            if (goalsList.Count == 0)
            {
                return stats;
            }
            
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            double totalCompletionRate = 0;
            
            foreach (var goal in goalsList)
            {
                var actualCount = await _weeklyGoalRepository.GetApplicationCountForWeekAsync(
                    userId, goal.WeekStartDate, goal.WeekEndDate);
                
                var completionRate = goal.TargetApplicationCount > 0 
                    ? Math.Round((double)actualCount / goal.TargetApplicationCount * 100, 0)
                    : 0;
                
                totalCompletionRate += completionRate;
                
                if (actualCount >= goal.TargetApplicationCount)
                {
                    stats.CompletedGoals++;
                }
                
                if (goal.WeekStartDate <= today && goal.WeekEndDate >= today)
                {
                    stats.ActiveGoals++;
                    stats.CurrentWeekTarget = goal.TargetApplicationCount;
                    stats.CurrentWeekActual = actualCount;
                    stats.CurrentWeekProgress = completionRate;
                }
            }
            
            stats.AverageCompletionRate = Math.Round(totalCompletionRate / goalsList.Count, 0);
            
            return stats;
        }

        private async Task<WeeklyGoalResponseDto> MapToResponseDtoAsync(WeeklyGoal weeklyGoal)
        {
            var dto = _mapper.Map<WeeklyGoalResponseDto>(weeklyGoal);
            
            // Get actual application count for the week
            dto.ActualApplicationCount = await _weeklyGoalRepository.GetApplicationCountForWeekAsync(
                weeklyGoal.UserId, 
                weeklyGoal.WeekStartDate, 
                weeklyGoal.WeekEndDate);
            
            // Calculate progress
            dto.ProgressPercentage = weeklyGoal.TargetApplicationCount > 0 
                ? Math.Round((double)dto.ActualApplicationCount / weeklyGoal.TargetApplicationCount * 100, 0)
                : 0;
            
            dto.IsCompleted = dto.ActualApplicationCount >= weeklyGoal.TargetApplicationCount;
            
            return dto;
        }
    }
}

