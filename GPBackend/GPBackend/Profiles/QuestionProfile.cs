using AutoMapper;
using GPBackend.DTOs.Question;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            // Map from Question to QuestionResponseDto
            CreateMap<Question, QuestionResponseDto>();

            // Map from QuestionCreateDto to Question
            CreateMap<QuestionCreateDto, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // Map from QuestionUpdateDto to Question
            CreateMap<QuestionUpdateDto, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Question1, opt => opt.Condition(src => src.Question1 != null))
                .ForMember(dest => dest.Answer, opt => opt.Condition(src => src.Answer != null));
                
        }
    }
} 