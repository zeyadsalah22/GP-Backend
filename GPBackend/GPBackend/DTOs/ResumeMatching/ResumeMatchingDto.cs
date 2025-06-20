using System;
using System.Collections.Generic;
using GPBackend.Models;

namespace GPBackend.DTOs.ResumeMatching
{
    public class ResumeMatchingRequestDto
    {
        public int ResumeId { get; set; }
        public string JobDescription { get; set; }
    }

    public class ResumeMatchingResponseDto
    {
        public int TestId { get; set; }
        public int ResumeId { get; set; }
        public int AtsScore { get; set; }
        public DateTime TestDate { get; set; }
        public string JobDescription { get; set; }
        public List<string> BestMatchingSkills { get; set; }
        public List<string> MissingSkills { get; set; }
    }

    // DTOs for AI Model Communication
    internal class SkillMatchingRequestDto
    {
        public string Skills { get; set; }
        public string JobDescription { get; set; }
    }

    internal class SkillMatchingResponseDto
    {
        public double Score { get; set; }
        public List<string> BestMatchingSkills { get; set; }
        public List<string> MissingSkills { get; set; }
    }

    public class ResumeTestListItemDto
    {
        public int TestId { get; set; }
        public int ResumeId { get; set; }
        public int AtsScore { get; set; }
        public DateTime TestDate { get; set; }
        public string JobDescription { get; set; }
    }

    public class ResumeTestDetailDto
    {
        public int TestId { get; set; }
        public int ResumeId { get; set; }
        public int AtsScore { get; set; }
        public DateTime TestDate { get; set; }
        public string JobDescription { get; set; }
        public List<string> MissingSkills { get; set; }
    }
} 
