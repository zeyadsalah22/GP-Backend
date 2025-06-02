using AutoMapper;
using GPBackend.DTOs.Interview;
using GPBackend.Models;
using GPBackend.DTOs.InterviewQuestion;

namespace GPBackend.Profiles
{
    public class InterviewProfile : Profile
    {
        public InterviewProfile()
        {
            // Map from Interview to InterviewResponseDto
            CreateMap<Interview, InterviewResponseDto>()
                .ForMember(dest => dest.InterviewId, opt => opt.MapFrom(src => src.InterviewId))
                .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.ApplicationId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.InterviewQuestions, opt => opt.MapFrom(src => src.InterviewQuestions));

            // Map from InterviewQuestionUpdateDto to InterviewQuestion
            CreateMap<InterviewQuestionUpdateDto, InterviewQuestion>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer))
                .ForMember(dest => dest.InterviewId, opt => opt.MapFrom(src => src.InterviewId));

            // Map from InterviewCreateDto to Interview
            CreateMap<InterviewCreateDto, Interview>()
                .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.ApplicationId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));


            // Map from InterviewUpdateDto to Interview
            CreateMap<InterviewUpdateDto, Interview>()
                .ForMember(dest => dest.InterviewId, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                .ForMember(dest => dest.Position, opt => opt.Ignore())
                .ForMember(dest => dest.Feedback, opt => opt.MapFrom(src => src.Feedback))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        }
    }
}