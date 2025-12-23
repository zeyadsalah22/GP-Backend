using AutoMapper;
using GPBackend.DTOs.Post;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            // Tag mappings
            CreateMap<Tag, TagDto>();

            // Post to DTO mappings - handled manually in service for anonymous logic

            // DTO to Post mappings
            CreateMap<PostCreateDto, Post>()
                .ForMember(dest => dest.PostId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.PostTags, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore());

            CreateMap<PostUpdateDto, Post>()
                .ForMember(dest => dest.PostId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.PostTags, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

