using AutoMapper;
using GPBackend.DTOs.Application;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            // Map from Application to ApplicationResponseDto
            CreateMap<Application, ApplicationResponseDto>();
                
            // Map from ApplicationCreateDto to Application
            CreateMap<ApplicationCreateDto, Application>()
                .ForMember(dest => dest.ApplicationId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationEmployees, opt => opt.Ignore())
                .ForMember(dest => dest.Interviews, opt => opt.Ignore())
                .ForMember(dest => dest.Question, opt => opt.Ignore())
                .ForMember(dest => dest.SubmittedCv, opt => opt.Ignore())
                .ForMember(dest => dest.UserCompany, opt => opt.Ignore());
                
            // Map from ApplicationUpdateDto to Application
            CreateMap<ApplicationUpdateDto, Application>()
                .ForMember(dest => dest.ApplicationId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationEmployees, opt => opt.Ignore())
                .ForMember(dest => dest.Interviews, opt => opt.Ignore())
                .ForMember(dest => dest.Question, opt => opt.Ignore())
                .ForMember(dest => dest.SubmittedCv, opt => opt.Ignore())
                .ForMember(dest => dest.UserCompany, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
} 