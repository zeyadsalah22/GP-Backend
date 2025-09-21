using AutoMapper;
using GPBackend.DTOs.Application;
using GPBackend.DTOs.Company;
using GPBackend.DTOs.Employee;
using GPBackend.Models;
using GPBackend.Models.Enums;

namespace GPBackend.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            // Map from Application to ApplicationResponseDto
            CreateMap<Application, ApplicationResponseDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.UserCompany.Company.Name))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.UserCompany.Company))
                .ForMember(dest => dest.ContactedEmployees, opt => opt.Ignore()) // We handle this manually in the service
                .ForMember(dest => dest.Timeline, opt => opt.MapFrom(src => src.StageHistory));

            CreateMap<ApplicationStageHistory, ApplicationStageHistoryDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.ReachedDate));
                
            // Map from Company to CompanyResponseDto for nested mapping
            CreateMap<Company, CompanyResponseDto>();
            
            // Map from Employee to EmployeeDto for nested mapping
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.UserCompany.Company.Name))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.UserCompany.Company));
                
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
                .ForMember(dest => dest.Questions, opt => opt.Ignore())
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
                .ForMember(dest => dest.Questions, opt => opt.Ignore())
                .ForMember(dest => dest.SubmittedCv, opt => opt.Ignore())
                .ForMember(dest => dest.UserCompany, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
} 