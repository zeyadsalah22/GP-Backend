using AutoMapper;
using GPBackend.Models;
using GPBackend.DTOs.Comment;

namespace GPBackend.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentCreateDto, Comment>()
                .ForMember(dest => dest.CommentId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Level, opt => opt.Ignore())
                .ForMember(dest => dest.ReplyCount, opt => opt.Ignore())
                .ForMember(dest => dest.IsEdited, opt => opt.Ignore())
                .ForMember(dest => dest.LastEditedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Rowversion, opt => opt.Ignore())
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.ParentComment, opt => opt.Ignore())
                .ForMember(dest => dest.Replies, opt => opt.Ignore())
                .ForMember(dest => dest.EditHistory, opt => opt.Ignore())
                .ForMember(dest => dest.Mentions, opt => opt.Ignore());

            CreateMap<Comment, CommentResponseDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src =>
                    src.IsDeleted ? "[Deleted]" : $"{src.User.Fname} {src.User.Lname}"))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src =>
                    src.IsDeleted ? "[Deleted comment]" : src.Content))
                .ForMember(dest => dest.ParentAuthorName, opt => opt.Ignore())
                .ForMember(dest => dest.ParentContentPreview, opt => opt.Ignore())
                .ForMember(dest => dest.EditedTimeAgo, opt => opt.Ignore())
                .ForMember(dest => dest.TimeAgo, opt => opt.Ignore())
                .ForMember(dest => dest.Mentions, opt => opt.Ignore())
                .ForMember(dest => dest.Replies, opt => opt.Ignore());
        }
    }
}


