using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class SkillRepository : ISkillRepository
    {
        private readonly GPDBContext _context;

        public SkillRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<List<Skill>> CreateAsync(List<Skill> skills)
        {
            _context.Skills.AddRange(skills);
            await _context.SaveChangesAsync();
            return skills;
        }

        public async Task<List<Skill>> GetByTestIdAsync(int testId)
        {
            return await _context.Skills
                .Where(s => s.TestId == testId)
                .ToListAsync();
        }

        public async Task<Skill?> GetByIdAsync(int skillId)
        {
            return await _context.Skills
                .FirstOrDefaultAsync(s => s.SkillId == skillId);
        }

        public async Task<bool> UpdateAsync(Skill skill)
        {
            try
            {
                _context.Skills.Update(skill);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int skillId)
        {
            var skill = await GetByIdAsync(skillId);
            if (skill == null)
            {
                return false;
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 