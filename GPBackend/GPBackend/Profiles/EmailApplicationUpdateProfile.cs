using AutoMapper;
using GPBackend.DTOs.Gmail;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class EmailApplicationUpdateProfile : Profile
    {
        public EmailApplicationUpdateProfile()
        {
            // Map from EmailApplicationUpdate entity to response DTO
            CreateMap<EmailApplicationUpdate, EmailApplicationUpdateResponseDto>()
                .ForMember(dest => dest.ApplicationJobTitle, 
                    opt => opt.MapFrom(src => src.Application.JobTitle))
                .ForMember(dest => dest.ApplicationCompanyName, 
                    opt => opt.MapFrom(src => src.Application.UserCompany.Company.Name));
        }
    }
}

