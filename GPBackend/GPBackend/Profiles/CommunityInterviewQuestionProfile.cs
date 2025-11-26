using AutoMapper;
using GPBackend.DTOs.CommunityInterviewQuestion;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class CommunityInterviewQuestionProfile : Profile
    {
        public CommunityInterviewQuestionProfile()
        {
            CreateMap<CommunityInterviewQuestionCreateDto, CommunityInterviewQuestion>();

            CreateMap<CommunityInterviewQuestion, CommunityInterviewQuestionResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.Fname} {src.User.Lname}"))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : src.CompanyName))
                .ForMember(dest => dest.CompanyLogo, opt => opt.MapFrom(src => src.Company != null ? src.Company.LogoUrl : src.CompanyLogo));
        }
    }
}

