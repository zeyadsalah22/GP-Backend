using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPBackend.DTOs;
using GPBackend.Models;
using Microsoft.EntityFrameworkCore;
using GPBackend.Models.Enums;

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

            var total_applications = applications.Count;
            var rejected_applications = applications.Count(a => a.Status == ApplicationDecisionStatus.Rejected);
            var accepted_applications = applications.Count(a => a.Status == ApplicationDecisionStatus.Accepted);

            var result = new StatisticsDTO
            {
                total_applications = total_applications,
                rejected_applications = rejected_applications,
                accepted_applications = accepted_applications,
                pending_applications = total_applications - (rejected_applications + accepted_applications),
                last_application = applications.OrderByDescending(a => a.SubmissionDate).FirstOrDefault()?.SubmissionDate != null
                    ? applications.OrderByDescending(a => a.SubmissionDate).FirstOrDefault().SubmissionDate.ToDateTime(TimeOnly.MinValue)
                    : null,
                last_rejection = applications.Where(a => a.Status == ApplicationDecisionStatus.Rejected).OrderByDescending(a => a.SubmissionDate).FirstOrDefault()?.SubmissionDate != null
                    ? applications.Where(a => a.Status == ApplicationDecisionStatus.Rejected).OrderByDescending(a => a.SubmissionDate).FirstOrDefault().SubmissionDate.ToDateTime(TimeOnly.MinValue)
                    : null,
                last_acceptance = applications.Where(a => a.Status == ApplicationDecisionStatus.Accepted).OrderByDescending(a => a.SubmissionDate).FirstOrDefault()?.SubmissionDate != null
                    ? applications.Where(a => a.Status == ApplicationDecisionStatus.Accepted).OrderByDescending(a => a.SubmissionDate).FirstOrDefault().SubmissionDate.ToDateTime(TimeOnly.MinValue)
                    : null,
                last_pending = applications.Where(a => a.Status == ApplicationDecisionStatus.Pending).OrderByDescending(a => a.SubmissionDate).FirstOrDefault()?.SubmissionDate != null
                    ? applications.Where(a => a.Status == ApplicationDecisionStatus.Pending).OrderByDescending(a => a.SubmissionDate).FirstOrDefault().SubmissionDate.ToDateTime(TimeOnly.MinValue)
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

                var total_applications = applications.Count(a => a.SubmissionDate >= currentDateOnly &&
                                                          a.SubmissionDate < nextDateOnly);
                var rejections = applications.Count(a => a.Status == ApplicationDecisionStatus.Rejected &&
                                            a.SubmissionDate >= currentDateOnly &&
                                            a.SubmissionDate < nextDateOnly);
                var acceptances = applications.Count(a => a.Status == ApplicationDecisionStatus.Accepted &&
                                                 a.SubmissionDate >= currentDateOnly &&
                                                 a.SubmissionDate < nextDateOnly);

                var pointData = new TimeSeriesPointDTO
                {
                    date = currentDate,
                    total_applications = total_applications,
                    rejections = rejections,
                    acceptances = acceptances,
                    pending = total_applications - (rejections + acceptances),
                };

                timeSeriesDTO.results.Add(pointData);
                currentDate = nextDate;
            }

            return timeSeriesDTO;
        }

        public async Task<PercentsDTO> GetPercentsAsync(int userId)
        {
            var applications = await _context.Applications
                .Where(a => a.UserId == userId && !a.IsDeleted && (a.Status == ApplicationDecisionStatus.Accepted || a.Status == ApplicationDecisionStatus.Rejected))
                .Select(a => new
                {
                    a.Stage
                })
                .ToListAsync();

            var totalCount = applications.Count;

            var percentsDTO = new PercentsDTO
            {
                total_applications = totalCount,
                applied_stage = applications.Count(a => a.Stage == ApplicationStage.Applied),
                phonescreen_stage = applications.Count(a => a.Stage == ApplicationStage.PhoneScreen),
                assessment_stage = applications.Count(a => a.Stage == ApplicationStage.Assessment),
                interview_stage = applications.Count(a => a.Stage == ApplicationStage.HrInterview) + applications.Count(a => a.Stage == ApplicationStage.TechnicalInterview),
                offer_stage = applications.Count(a => a.Stage == ApplicationStage.Offer)
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