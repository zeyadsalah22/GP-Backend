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

            // Map from InterviewResponseDto to Interview
            CreateMap<InterviewResponseDto, Interview>()
                .ForMember(dest => dest.InterviewId, opt => opt.MapFrom(src => src.InterviewId))
                .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.ApplicationId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Feedback, opt => opt.MapFrom(src => src.Feedback))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.JobDescription))
                .ForMember(dest => dest.InterviewQuestions, opt => opt.MapFrom(src => src.InterviewQuestions));

            // // Map from InterviewQuestionUpdateDto to InterviewQuestion
            // CreateMap<InterviewQuestionUpdateDto, InterviewQuestion>()
            //     .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.Id))
            //     .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer))
            //     .ForMember(dest => dest.InterviewId, opt => opt.MapFrom(src => src.InterviewId));

            // Map from InterviewCreateDto to Interview
            CreateMap<InterviewCreateDto, Interview>()
                .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.ApplicationId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => 60)) // Default duration set to 60 minutes
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.ApplicationId, opt => opt.Condition(src => src.ApplicationId != null))
                .ForMember(dest => dest.CompanyId, opt => opt.Condition(src => src.CompanyId != null))
                .ForMember(dest => dest.Position, opt => opt.Condition(src => src.Position != null))
                .ForMember(dest => dest.JobDescription, opt => opt.Condition(src => src.JobDescription != null));



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
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.ApplicationId, opt => opt.Condition(src => src.ApplicationId != null))
                .ForMember(dest => dest.CompanyId, opt => opt.Condition(src => src.CompanyId != null))
                .ForMember(dest => dest.Position, opt => opt.Condition(src => src.Position != null))
                .ForMember(dest => dest.JobDescription, opt => opt.Condition(src => src.JobDescription != null))
                .ForMember(dest => dest.InterviewQuestions, opt => opt.Condition(src => src.InterviewQuestions != null))
                .ForMember(dest => dest.Duration, opt => opt.Condition(src => src.Duration != null))
                .ForMember(dest => dest.StartDate, opt => opt.Condition(src => src.StartDate != null))
                .ForMember(dest => dest.Feedback, opt => opt.Condition(src => src.Feedback != null));

                // .ForMember(dest => dest.InterviewQuestions, opt => opt.MapFrom(src => src.InterviewQuestions));

        }
    }
}