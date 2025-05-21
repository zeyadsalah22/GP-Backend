using System;
using System.Threading.Tasks;
using GPBackend.DTOs;

namespace GPBackend.Services
{
    public interface IInsightsService
    {
        Task<StatisticsDTO> GetStatisticsAsync(int userId);
        Task<TimeSeriesDTO> GetTimeSeriesAsync(int userId, DateTime? startDate, int? points, string interval);
        Task<PercentsDTO> GetPercentsAsync(int userId);
    }
} 