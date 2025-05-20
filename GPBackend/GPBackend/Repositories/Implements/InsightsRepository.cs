using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPBackend.DTOs;
using GPBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories
{
    public class InsightsRepository : IInsightsRepository
    {
        private readonly GPDBContext _context;

        public InsightsRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<StatisticsDTO> GetStatisticsAsync(int userId)
        {
            var applications = await _context.Applications
                .Where(a => a.UserId == userId && !a.IsDeleted)
                .Select(a => new
                {
                    a.Status,
                    a.SubmissionDate
                })
                .ToListAsync();

            var result = new StatisticsDTO
            {
                total_applications = applications.Count,
                rejected_applications = applications.Count(a => a.Status.ToLower() == "rejected"),
                pending_applications = applications.Count(a => a.Status.ToLower() == "pending"),
                accepted_applications = applications.Count(a => a.Status.ToLower() == "accepted"),
                last_application = applications.OrderByDescending(a => a.SubmissionDate).FirstOrDefault()?.SubmissionDate != null 
                    ? applications.OrderByDescending(a => a.SubmissionDate).FirstOrDefault().SubmissionDate.ToDateTime(TimeOnly.MinValue) 
                    : null,
                last_rejection = applications.Where(a => a.Status.ToLower() == "rejected").OrderByDescending(a => a.SubmissionDate).FirstOrDefault()?.SubmissionDate != null 
                    ? applications.Where(a => a.Status.ToLower() == "rejected").OrderByDescending(a => a.SubmissionDate).FirstOrDefault().SubmissionDate.ToDateTime(TimeOnly.MinValue) 
                    : null,
                last_acceptance = applications.Where(a => a.Status.ToLower() == "accepted").OrderByDescending(a => a.SubmissionDate).FirstOrDefault()?.SubmissionDate != null 
                    ? applications.Where(a => a.Status.ToLower() == "accepted").OrderByDescending(a => a.SubmissionDate).FirstOrDefault().SubmissionDate.ToDateTime(TimeOnly.MinValue) 
                    : null,
                last_pending = applications.Where(a => a.Status.ToLower() == "pending").OrderByDescending(a => a.SubmissionDate).FirstOrDefault()?.SubmissionDate != null 
                    ? applications.Where(a => a.Status.ToLower() == "pending").OrderByDescending(a => a.SubmissionDate).FirstOrDefault().SubmissionDate.ToDateTime(TimeOnly.MinValue) 
                    : null
            };

            return result;
        }

        public async Task<TimeSeriesDTO> GetTimeSeriesAsync(int userId, DateTime startDate, int points, string interval)
        {
            var dateOnlyStartDate = DateOnly.FromDateTime(startDate);
            
            var applications = await _context.Applications
                .Where(a => a.UserId == userId && !a.IsDeleted && a.SubmissionDate >= dateOnlyStartDate)
                .Select(a => new
                {
                    a.Status,
                    a.SubmissionDate
                })
                .ToListAsync();

            var timeSeriesDTO = new TimeSeriesDTO
            {
                points = points,
                start_date = startDate,
                interval = interval,
                results = new List<TimeSeriesPointDTO>()
            };

            // Calculate end date based on interval and points
            DateTime endDate = CalculateEndDate(startDate, interval, points);
            var currentDate = startDate;

            while (currentDate <= endDate && timeSeriesDTO.results.Count < points)
            {
                DateTime nextDate = GetNextDate(currentDate, interval);
                DateOnly currentDateOnly = DateOnly.FromDateTime(currentDate);
                DateOnly nextDateOnly = DateOnly.FromDateTime(nextDate);
                
                var pointData = new TimeSeriesPointDTO
                {
                    date = currentDate,
                    total_applications = applications.Count(a => a.SubmissionDate >= currentDateOnly && 
                                                          a.SubmissionDate < nextDateOnly),
                    rejections = applications.Count(a => a.Status.ToLower() == "rejected" && 
                                                a.SubmissionDate >= currentDateOnly && 
                                                a.SubmissionDate < nextDateOnly),
                    acceptances = applications.Count(a => a.Status.ToLower() == "accepted" && 
                                                 a.SubmissionDate >= currentDateOnly && 
                                                 a.SubmissionDate < nextDateOnly)
                };

                timeSeriesDTO.results.Add(pointData);
                currentDate = nextDate;
            }

            return timeSeriesDTO;
        }

        public async Task<PercentsDTO> GetPercentsAsync(int userId)
        {
            var applications = await _context.Applications
                .Where(a => a.UserId == userId && !a.IsDeleted && (a.Status.ToLower() == "accepted" || a.Status.ToLower() == "rejected"))
                .Select(a => new
                {
                    a.Stage
                })
                .ToListAsync();

            var totalCount = applications.Count;

            var percentsDTO = new PercentsDTO
            {
                total_applications = totalCount,
                applied_stage = applications.Count(a => a.Stage.ToLower() == "applied"),
                phonescreen_stage = applications.Count(a => a.Stage.ToLower() == "phonescreen"),
                assessment_stage = applications.Count(a => a.Stage.ToLower() == "assessment"),
                interview_stage = applications.Count(a => a.Stage.ToLower() == "interview"),
                offer_stage = applications.Count(a => a.Stage.ToLower() == "offer")
            };

            return percentsDTO;
        }

        private DateTime CalculateEndDate(DateTime startDate, string interval, int points)
        {
            return interval.ToLower() switch
            {
                "day" => startDate.AddDays(points),
                "week" => startDate.AddDays(points * 7),
                "month" => startDate.AddMonths(points),
                _ => startDate.AddMonths(points)
            };
        }

        private DateTime GetNextDate(DateTime currentDate, string interval)
        {
            return interval.ToLower() switch
            {
                "day" => currentDate.AddDays(1),
                "week" => currentDate.AddDays(7),
                "month" => currentDate.AddMonths(1),
                _ => currentDate.AddMonths(1)
            };
        }
    }
} 