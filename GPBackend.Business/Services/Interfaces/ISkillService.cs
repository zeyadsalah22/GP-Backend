using GPBackend.DTOs.Skill;

namespace GPBackend.Services.Interfaces
{
    public interface ISkillService
    {
        Task<SkillResponseDto?> CreateSkillAsync(int userId, SkillCreateDto createDto);
        Task<SkillResponseDto?> UpdateSkillAsync(int userId, int skillId, SkillUpdateDto updateDto);
        Task<bool> DeleteSkillAsync(int userId, int skillId);
    }
} 