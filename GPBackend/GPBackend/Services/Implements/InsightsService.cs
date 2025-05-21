using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPBackend.DTOs;
using GPBackend.Repositories;

namespace GPBackend.Services
{
    public class InsightsService : IInsightsService
    {
        private readonly IInsightsRepository _insightsRepository;

        public InsightsService(IInsightsRepository insightsRepository)
        {
            _insightsRepository = insightsRepository;
        }

        public async Task<StatisticsDTO> GetStatisticsAsync(int userId)
        {
            return await _insightsRepository.GetStatisticsAsync(userId);
        }

        public async Task<TimeSeriesDTO> GetTimeSeriesAsync(int userId, DateTime? startDate, int? points, string interval)
        {
            // Set default values if not provided
            DateTime effectiveStartDate = startDate ?? DateTime.Now.AddDays(-84);
            int effectivePoints = points ?? 12;
            string effectiveInterval = !string.IsNullOrEmpty(interval) ? interval.ToLower() : "week";

            // Validate interval
            if (effectiveInterval != "day" && effectiveInterval != "week" && effectiveInterval != "month")
            {
                throw new ArgumentException("Invalid interval. Choose from: month, week, day");
            }

            // Validate points (range check)
            if (effectivePoints < 1 || effectivePoints > 100)
            {
                throw new ArgumentException("Invalid number of points. Choose from 1 to 100");
            }

            return await _insightsRepository.GetTimeSeriesAsync(userId, effectiveStartDate, effectivePoints, effectiveInterval);
        }

        public async Task<PercentsDTO> GetPercentsAsync(int userId)
        {
            return await _insightsRepository.GetPercentsAsync(userId);
        }
    }
} 