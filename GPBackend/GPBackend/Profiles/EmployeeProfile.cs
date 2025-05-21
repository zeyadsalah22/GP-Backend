using AutoMapper;
using GPBackend.Models;
using GPBackend.DTOs.Employee;

namespace GPBackend.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Map from Entity to DTO
            CreateMap<Employee, EmployeeDto>();

            // Map from CreationDto to Entity
            CreateMap<EmployeeCreationDto, Employee>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

            // Map from UpdateDto to Entity
            CreateMap<EmployeeUpdateDto, Employee>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
} 
