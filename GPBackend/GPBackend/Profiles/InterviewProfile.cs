using AutoMapper;
using GPBackend.DTOs.Interview;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class InterviewProfile : Profile
    {
        public InterviewProfile()
        {
            // Map from Interview to InterviewResponseDto
            CreateMap<Interview, InterviewResponseDto>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.InterviewQuestions));
            
        }
    }
}