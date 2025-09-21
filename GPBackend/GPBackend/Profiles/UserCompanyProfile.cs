using AutoMapper;
using GPBackend.DTOs.UserCompany;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class UserCompanyProfile : Profile
    {
        public UserCompanyProfile()
        {
            // Domain to DTO
            CreateMap<UserCompany, UserCompanyResponseDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.CompanyLocation, opt => opt.MapFrom(src => src.Company.Location))
                .ForMember(dest => dest.CompanyCareersLink, opt => opt.MapFrom(src => src.Company.CareersLink))
                .ForMember(dest => dest.CompanyLinkedinLink, opt => opt.MapFrom(src => src.Company.LinkedinLink))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag).ToList()));

            // DTO to Domain
            CreateMap<UserCompanyCreateDto, UserCompany>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Company, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Applications, opt => opt.Ignore())
                .ForMember(dest => dest.Employees, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore());

            CreateMap<UserCompanyUpdateDto, UserCompany>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                .ForMember(dest => dest.Company, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Applications, opt => opt.Ignore())
                .ForMember(dest => dest.Employees, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore());
        }
    }
} 