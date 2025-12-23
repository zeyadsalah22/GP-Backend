using AutoMapper;
using GPBackend.DTOs.Auth;
using GPBackend.DTOs.User;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Map from User model to UserResponseDto
            CreateMap<User, UserResponseDto>();
            
            // Map from UserResponseDto to User model (for JWT generation)
            CreateMap<UserResponseDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForMember(dest => dest.Interviews, opt => opt.Ignore())
                .ForMember(dest => dest.Resumes, opt => opt.Ignore())
                .ForMember(dest => dest.TodoLists, opt => opt.Ignore())
                .ForMember(dest => dest.UserCompanies, opt => opt.Ignore());
            
            // Map from UserUpdateDto to User model
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForMember(dest => dest.Interviews, opt => opt.Ignore())
                .ForMember(dest => dest.Resumes, opt => opt.Ignore())
                .ForMember(dest => dest.TodoLists, opt => opt.Ignore())
                .ForMember(dest => dest.UserCompanies, opt => opt.Ignore());
            
            // Map from RegisterDto to User model (already handled in Auth service likely, but good to have)
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForMember(dest => dest.Interviews, opt => opt.Ignore())
                .ForMember(dest => dest.Resumes, opt => opt.Ignore())
                .ForMember(dest => dest.TodoLists, opt => opt.Ignore())
                .ForMember(dest => dest.UserCompanies, opt => opt.Ignore());
        }
    }
} 