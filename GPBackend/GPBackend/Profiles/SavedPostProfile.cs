using AutoMapper;
using GPBackend.Models;
using GPBackend.DTOs.SavedPost;

namespace GPBackend.Profiles
{
    public class SavedPostProfile : Profile
    {
        public SavedPostProfile()
        {
            CreateMap<SavedPost, SavedPostResponseDto>()
                .ForMember(dest => dest.Post, opt => opt.Ignore());
        }
    }
}

