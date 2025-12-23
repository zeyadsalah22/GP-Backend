using AutoMapper;
using GPBackend.DTOs.Interview;
using GPBackend.Models;
using GPBackend.DTOs.InterviewQuestion;
using GPBackend.DTOs.Notification;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GPBackend.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationResponseDto>();

            CreateMap<NotificationCreateDto, Notification>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Equals(DateTime.Now));

            CreateMap<NotificationUpdateDto, Notification>();
        }
    }
}