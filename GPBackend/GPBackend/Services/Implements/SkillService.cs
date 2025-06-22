using GPBackend.DTOs.Skill;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IResumeTestRepository _resumeTestRepository;

        public SkillService(ISkillRepository skillRepository, IResumeTestRepository resumeTestRepository)
        {
            _skillRepository = skillRepository;
            _resumeTestRepository = resumeTestRepository;
        }

        public async Task<SkillResponseDto?> CreateSkillAsync(int userId, SkillCreateDto createDto)
        {
            // Verify that the test belongs to the user
            var resumeTest = await _resumeTestRepository.GetResumeTestByIdAsync(createDto.TestId, userId);
            if (resumeTest == null)
            {
                return null; // Test not found or doesn't belong to the user
            }

            var skill = new Skill
            {
                TestId = createDto.TestId,
                Skill1 = createDto.SkillName
            };

            var skills = new List<Skill> { skill };
            var createdSkills = await _skillRepository.CreateAsync(skills);

            if (createdSkills.Count == 0)
            {
                return null;
            }

            return new SkillResponseDto
            {
                SkillId = createdSkills[0].SkillId,
                TestId = createdSkills[0].TestId,
                SkillName = createdSkills[0].Skill1
            };
        }

        public async Task<SkillResponseDto?> UpdateSkillAsync(int userId, int skillId, SkillUpdateDto updateDto)
        {
            // Get the skill
            var skill = await _skillRepository.GetByIdAsync(skillId);
            if (skill == null)
            {
                return null;
            }

            // Verify that the test belongs to the user
            var resumeTest = await _resumeTestRepository.GetResumeTestByIdAsync(skill.TestId, userId);
            if (resumeTest == null)
            {
                return null; // Test not found or doesn't belong to the user
            }

            // Update the skill
            skill.Skill1 = updateDto.SkillName;
            var success = await _skillRepository.UpdateAsync(skill);

            if (!success)
            {
                return null;
            }

            return new SkillResponseDto
            {
                SkillId = skill.SkillId,
                TestId = skill.TestId,
                SkillName = skill.Skill1
            };
        }

        public async Task<bool> DeleteSkillAsync(int userId, int skillId)
        {
            // Get the skill
            var skill = await _skillRepository.GetByIdAsync(skillId);
            if (skill == null)
            {
                return false;
            }

            // Verify that the test belongs to the user
            var resumeTest = await _resumeTestRepository.GetResumeTestByIdAsync(skill.TestId, userId);
            if (resumeTest == null)
            {
                return false; // Test not found or doesn't belong to the user
            }

            return await _skillRepository.DeleteAsync(skillId);
        }
    }
} 