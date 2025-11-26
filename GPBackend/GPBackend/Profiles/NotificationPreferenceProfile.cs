using AutoMapper;
using GPBackend.DTOs.Interview;
using GPBackend.Models;
using GPBackend.DTOs.InterviewQuestion;
using GPBackend.DTOs.Notification;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GPBackend.Profiles
{
    public class NotificationPreferenceProfile : Profile
    {
        public NotificationPreferenceProfile()
        {
            CreateMap<NotificationPreference, NotificationPreferenceResponseDto>();

            CreateMap<NotificationPreferenceUpdateDto, NotificationPreference>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.Equals(DateTime.Now))
            .ForMember(dest => dest.EnableReminders, opt => opt.Condition(src => src.EnableReminders != null))
            .ForMember(dest => dest.EnableSocial, opt => opt.Condition(src => src.EnableSocial != null))
            .ForMember(dest => dest.EnableSystem, opt => opt.Condition(src => src.EnableSystem != null))
            .ForMember(dest => dest.GloballyEnabled, opt => opt.Condition(src => src.GloballyEnabled != null));
        }
    }
}