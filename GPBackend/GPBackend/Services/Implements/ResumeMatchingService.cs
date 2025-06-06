using System;
using System.Linq;
using System.Threading.Tasks;
using GPBackend.DTOs.ResumeMatching;
using GPBackend.Models;
using GPBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Services.Implements
{
    public class ResumeMatchingService : IResumeMatchingService
    {
        private readonly GPDBContext _context;
        private readonly ISkillExtractionService _skillExtractor;
        private readonly ISkillMatchingApiClient _matchingClient;

        public ResumeMatchingService(
            GPDBContext context,
            ISkillExtractionService skillExtractor,
            ISkillMatchingApiClient matchingClient)
        {
            _context = context;
            _skillExtractor = skillExtractor;
            _matchingClient = matchingClient;
        }

        public async Task<ResumeMatchingResponseDto> MatchResumeAsync(ResumeMatchingRequestDto request)
        {
            try
            {
                // 1. Get the resume
                var resume = await _context.Resumes
                    .FirstOrDefaultAsync(r => r.ResumeId == request.ResumeId);

                if (resume == null)
                {
                    throw new ArgumentException($"Resume with ID {request.ResumeId} not found.");
                }

                // 2. Extract skills from resume
                var extractedSkills = await _skillExtractor.ExtractSkillsAsync(resume.ResumeFile);

                if (!extractedSkills.Any())
                {
                    throw new Exception("No skills could be extracted from the resume.");
                }

                // 3. Get matching score from AI model
                var score = await _matchingClient.GetMatchingScoreAsync(
                    string.Join(" ", extractedSkills),
                    request.JobDescription
                );

                // 4. Create new resume test
                var resumeTest = new ResumeTest
                {
                    ResumeId = request.ResumeId,
                    JobDescription = request.JobDescription,
                    TestDate = DateTime.UtcNow,
                    AtsScore = (int)(score * 100) // Convert score to percentage
                };

                _context.ResumeTests.Add(resumeTest);
                await _context.SaveChangesAsync();

                // 5. Add skills
                var skills = extractedSkills.Select(skill => new Skill
                {
                    TestId = resumeTest.TestId,
                    Skill1 = skill
                }).ToList();

                _context.Skills.AddRange(skills);
                await _context.SaveChangesAsync();

                // 6. Return response
                return new ResumeMatchingResponseDto
                {
                    TestId = resumeTest.TestId,
                    ResumeId = resumeTest.ResumeId,
                    AtsScore = resumeTest.AtsScore,
                    TestDate = resumeTest.TestDate,
                    JobDescription = resumeTest.JobDescription,
                    ExtractedSkills = extractedSkills
                };
            }
            catch (Exception ex)
            {
                // Log the error
                throw new Exception("Error processing resume matching request", ex);
            }
        }
    }
} 