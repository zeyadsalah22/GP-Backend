using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface ISkillRepository
    {
        Task<List<Skill>> CreateAsync(List<Skill> skills);
        Task<List<Skill>> GetByTestIdAsync(int testId);
        Task<Skill?> GetByIdAsync(int skillId);
        Task<bool> UpdateAsync(Skill skill);
        Task<bool> DeleteAsync(int skillId);
    }
} 