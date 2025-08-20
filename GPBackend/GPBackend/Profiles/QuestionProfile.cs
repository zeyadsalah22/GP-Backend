using AutoMapper;
using GPBackend.DTOs.Question;
using GPBackend.Models;
using System.Linq;

namespace GPBackend.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            // Map from Question to QuestionResponseDto
            CreateMap<Question, QuestionResponseDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag).ToList()));

            // Map from QuestionCreateDto to Question
            CreateMap<QuestionCreateDto, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Tags = new System.Collections.Generic.List<QuestionTag>();
                    if (src.Tags != null)
                    {
                        foreach (var tag in src.Tags.Distinct().Where(t => !string.IsNullOrWhiteSpace(t)))
                        {
                            dest.Tags.Add(new QuestionTag
                            {
                                Tag = tag.Trim(),
                                CreatedAt = System.DateTime.UtcNow,
                                UpdatedAt = System.DateTime.UtcNow
                            });
                        }
                    }
                });

            // Map from QuestionUpdateDto to Question
            CreateMap<QuestionUpdateDto, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Question1, opt => opt.Condition(src => src.Question1 != null))
                .ForMember(dest => dest.Answer, opt => opt.Condition(src => src.Answer != null))
                .ForMember(dest => dest.Favorite, opt => opt.Condition(src => src.Favorite.HasValue))
                .AfterMap((src, dest) =>
                {
                    if (src.Tags != null)
                    {
                        dest.Tags = dest.Tags ?? new System.Collections.Generic.List<QuestionTag>();
                        var existing = dest.Tags.ToList();

                        var newSet = src.Tags
                            .Where(t => !string.IsNullOrWhiteSpace(t))
                            .Select(t => t.Trim())
                            .Distinct()
                            .ToHashSet(System.StringComparer.OrdinalIgnoreCase);

                        // remove tags not in new set
                        dest.Tags = existing.Where(t => newSet.Contains(t.Tag)).ToList();

                        // add new tags
                        var existingSet = dest.Tags.Select(t => t.Tag).ToHashSet(System.StringComparer.OrdinalIgnoreCase);
                        foreach (var t in newSet)
                        {
                            if (!existingSet.Contains(t))
                            {
                                dest.Tags.Add(new QuestionTag
                                {
                                    Tag = t,
                                    CreatedAt = System.DateTime.UtcNow,
                                    UpdatedAt = System.DateTime.UtcNow
                                });
                            }
                        }
                    }
                });
                
        }
    }
} 