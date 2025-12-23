using System;
using System.Threading.Tasks;
using GPBackend.DTOs;

namespace GPBackend.Repositories
{
    public interface IInsightsRepository
    {
        Task<StatisticsDTO> GetStatisticsAsync(int userId);
        Task<TimeSeriesDTO> GetTimeSeriesAsync(int userId, DateTime startDate, int points, string interval);
        Task<PercentsDTO> GetPercentsAsync(int userId);
    }
} 