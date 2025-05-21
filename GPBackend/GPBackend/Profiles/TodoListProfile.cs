using AutoMapper;
using GPBackend.DTOs.TodoList;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            // Domain to DTO
            CreateMap<TodoList, TodoListResponseDto>();

            // DTO to Domain
            CreateMap<TodoListCreateDto, TodoList>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.TodoId, opt => opt.Ignore());

            CreateMap<TodoListUpdateDto, TodoList>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.TodoId, opt => opt.Ignore());
        }
    }
} 