using AutoMapper;
using GPBackend.DTOs.InterviewQuestion;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class InterviewQuestionProfile : Profile
    {
        public InterviewQuestionProfile()
        {
            // mapping from InterviewQuestion to InterviewQuestionResponseDto
            CreateMap<InterviewQuestion, InterviewQuestionResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer))
                .ForMember(dest => dest.InterviewId, opt => opt.MapFrom(src => src.InterviewId));

            // mapping from InterviewQuestionCreateDto to InterviewQuestion
            CreateMap<InterviewQuestionCreateDto, InterviewQuestion>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer))
                .ForMember(dest => dest.InterviewId, opt => opt.MapFrom(src => src.InterviewId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

            // mapping from InterviewQuestionResponseDto to InterviewQuestion
            CreateMap<InterviewQuestionResponseDto, InterviewQuestion>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer))
                .ForMember(dest => dest.InterviewId, opt => opt.MapFrom(src => src.InterviewId));

            // mapping from InterviewQuestionUpdateDto to InterviewQuestion
            CreateMap<InterviewQuestionUpdateDto, InterviewQuestion>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Question, opt => opt.Ignore())
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer))
                .ForMember(dest => dest.InterviewId, opt => opt.MapFrom(src => src.InterviewId))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));


            // mapping from InterviewQuestionResponseDto to InterviewQuestionCreateDto
            CreateMap<InterviewQuestionResponseDto, InterviewQuestionCreateDto>()
                .ForMember(dest => dest.InterviewId, opt => opt.MapFrom(src => src.InterviewId))
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer));

        }
    }
}
