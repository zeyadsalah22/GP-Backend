using AutoMapper;
using GPBackend.DTOs.Skill;
using GPBackend.Models;

namespace GPBackend.Profiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            // Skill mappings
            CreateMap<Skill, SkillResponseDto>()
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill1));

            CreateMap<SkillCreateDto, Skill>()
                .ForMember(dest => dest.SkillId, opt => opt.Ignore())
                .ForMember(dest => dest.Skill1, opt => opt.MapFrom(src => src.SkillName))
                .ForMember(dest => dest.Test, opt => opt.Ignore());

            CreateMap<SkillUpdateDto, Skill>()
                .ForMember(dest => dest.SkillId, opt => opt.Ignore())
                .ForMember(dest => dest.TestId, opt => opt.Ignore())
                .ForMember(dest => dest.Skill1, opt => opt.MapFrom(src => src.SkillName))
                .ForMember(dest => dest.Test, opt => opt.Ignore());
        }
    }
} 