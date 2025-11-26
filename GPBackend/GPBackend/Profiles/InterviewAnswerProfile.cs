using AutoMapper;
using GPBackend.DTOs.InterviewAnswer;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class InterviewAnswerProfile : Profile
    {
        public InterviewAnswerProfile()
        {
            CreateMap<InterviewAnswerCreateDto, InterviewAnswer>();

            CreateMap<InterviewAnswer, InterviewAnswerResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.Fname} {src.User.Lname}"))
                .ForMember(dest => dest.UserProfilePictureUrl, opt => opt.MapFrom(src => src.User.ProfilePictureUrl))
                .ForMember(dest => dest.CurrentUserMarkedHelpful, opt => opt.Ignore());
        }
    }
}

