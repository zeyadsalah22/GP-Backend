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

            // Calendar-align the start date so buckets match human expectations:
            // - day: 00:00 of that day
            // - week: start of week (Monday) 00:00
            // - month: first day of month 00:00
            DateTime effectiveStartDate = startDate.HasValue
                ? AlignToIntervalStart(startDate.Value, effectiveInterval)
                : GetDefaultAlignedStartDate(effectiveInterval, effectivePoints);

            return await _insightsRepository.GetTimeSeriesAsync(userId, effectiveStartDate, effectivePoints, effectiveInterval);
        }

        public async Task<PercentsDTO> GetPercentsAsync(int userId)
        {
            return await _insightsRepository.GetPercentsAsync(userId);
        }

        private static DateTime GetDefaultAlignedStartDate(string interval, int points)
        {
            // Use local "today" to avoid drifting times in the time-series output.
            var today = DateTime.Now.Date;

            return interval switch
            {
                // last N calendar days including today
                "day" => today.AddDays(-(points - 1)),
                // last N calendar weeks including current week
                "week" => StartOfWeek(today, DayOfWeek.Monday).AddDays(-7 * (points - 1)),
                // last N calendar months including current month
                "month" => new DateTime(today.Year, today.Month, 1, 0, 0, 0, today.Kind).AddMonths(-(points - 1)),
                _ => today.AddDays(-(points - 1))
            };
        }

        private static DateTime AlignToIntervalStart(DateTime input, string interval)
        {
            // Preserve DateTimeKind so JSON serialization keeps the same offset behavior.
            var date = input.Date;

            return interval switch
            {
                "day" => date,
                "week" => StartOfWeek(date, DayOfWeek.Monday),
                "month" => new DateTime(date.Year, date.Month, 1, 0, 0, 0, input.Kind),
                _ => date
            };
        }

        private static DateTime StartOfWeek(DateTime date, DayOfWeek startOfWeek)
        {
            var diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
            return date.AddDays(-diff);
        }
    }
} 