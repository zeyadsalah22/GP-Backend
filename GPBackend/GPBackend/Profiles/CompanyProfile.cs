using AutoMapper;
using GPBackend.DTOs.Company;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            // Domain to DTO
            CreateMap<Company, CompanyResponseDto>()
                .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Industry));

            // DTO to Domain
            CreateMap<CompanyCreateDto, Company>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForMember(dest => dest.Industry, opt => opt.Ignore())
                .ForMember(dest => dest.Interviews, opt => opt.Ignore())
                .ForMember(dest => dest.UserCompanies, opt => opt.Ignore());

            CreateMap<CompanyUpdateDto, Company>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForMember(dest => dest.Industry, opt => opt.Ignore())
                .ForMember(dest => dest.Interviews, opt => opt.Ignore())
                .ForMember(dest => dest.UserCompanies, opt => opt.Ignore());
        }
    }
} 