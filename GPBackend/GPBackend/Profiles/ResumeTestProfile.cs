using AutoMapper;
using GPBackend.DTOs.ResumeTest;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class ResumeTestProfile : Profile
    {
        public ResumeTestProfile()
        {
            // ResumeTest mappings
            CreateMap<ResumeTest, ResumeTestResponseDto>()
                .ForMember(dest => dest.MissingSkills, opt => opt.Ignore())
                .ForMember(dest => dest.MatchingSkills, opt => opt.Ignore());

            CreateMap<ResumeTestCreateDto, ResumeTest>()
                .ForMember(dest => dest.TestId, opt => opt.Ignore())
                .ForMember(dest => dest.AtsScore, opt => opt.Ignore())
                .ForMember(dest => dest.TestDate, opt => opt.Ignore())
                .ForMember(dest => dest.Resume, opt => opt.Ignore())
                .ForMember(dest => dest.Skills, opt => opt.Ignore());

            // ResumeTestQueryDto doesn't need mapping as it's for querying, not entity mapping
            // ResumeTestAIDto doesn't need mapping as it's for AI model communication, not entity mapping
        }
    }
} 