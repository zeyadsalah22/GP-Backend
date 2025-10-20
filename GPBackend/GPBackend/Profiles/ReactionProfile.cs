using AutoMapper;
using GPBackend.DTOs.Reaction;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class ReactionProfile : Profile
    {
        public ReactionProfile()
        {
            // PostReaction mappings
            CreateMap<PostReaction, PostReactionResponseDto>()
                .ForMember(dest => dest.ReactionTypeName, opt => opt.MapFrom(src => src.ReactionType.ToString()));

            CreateMap<PostReactionCreateDto, PostReaction>()
                .ForMember(dest => dest.PostReactionId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            // CommentReaction mappings
            CreateMap<CommentReaction, CommentReactionResponseDto>()
                .ForMember(dest => dest.ReactionTypeName, opt => opt.MapFrom(src => src.ReactionType.ToString()));

            CreateMap<CommentReactionCreateDto, CommentReaction>()
                .ForMember(dest => dest.CommentReactionId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForMember(dest => dest.Comment, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}

