using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GPBackend.Repositories.Implements
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly GPDBContext _context;

        public ResumeRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Resume>> GetAllAsync(int userId)
        {
            return await _context.Resumes
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Resume?> GetByIdAsync(int id)
        {
            return await _context.Resumes
                .Where(r => r.ResumeId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Resume> CreateAsync(Resume resume)
        {
            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();
            return resume;
        }


        public async Task<bool> UpdateAsync(Resume resume)
        {
            try
            {
                _context.Resumes.Update(resume);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ResumeExistsAsync(resume.ResumeId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resume = await GetByIdAsync(id);
            if (resume == null)
            {
                return false;
            }

            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> ResumeExistsAsync(int id)
        {
            return await _context.Resumes.AnyAsync(r => r.ResumeId == id);
        }
    }
}
