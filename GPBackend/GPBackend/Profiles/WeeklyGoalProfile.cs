using AutoMapper;
using GPBackend.DTOs.WeeklyGoal;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class WeeklyGoalProfile : Profile
    {
        public WeeklyGoalProfile()
        {
            // WeeklyGoal to WeeklyGoalResponseDto
            CreateMap<WeeklyGoal, WeeklyGoalResponseDto>()
                .ForMember(dest => dest.ActualApplicationCount, opt => opt.Ignore())
                .ForMember(dest => dest.ProgressPercentage, opt => opt.Ignore())
                .ForMember(dest => dest.IsCompleted, opt => opt.Ignore());
            
            // WeeklyGoalCreateDto to WeeklyGoal
            CreateMap<WeeklyGoalCreateDto, WeeklyGoal>()
                .ForMember(dest => dest.WeeklyGoalId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.WeekEndDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
            
            // WeeklyGoalUpdateDto to WeeklyGoal
            CreateMap<WeeklyGoalUpdateDto, WeeklyGoal>()
                .ForMember(dest => dest.WeeklyGoalId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.WeekStartDate, opt => opt.Ignore())
                .ForMember(dest => dest.WeekEndDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

