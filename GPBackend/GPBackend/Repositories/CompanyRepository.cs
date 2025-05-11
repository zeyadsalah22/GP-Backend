using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly GPDBContext _context;

        public CompanyRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _context.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == id);
        }

        public async Task<Company> CreateAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<bool> UpdateAsync(Company company)
        {
            var affected = await _context.Companies
                .Where(c => c.CompanyId == company.CompanyId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.Name, company.Name)
                    .SetProperty(c => c.Location, company.Location)
                    .SetProperty(c => c.CareersLink, company.CareersLink)
                    .SetProperty(c => c.LinkedinLink, company.LinkedinLink)
                    .SetProperty(c => c.UpdatedAt, DateTime.UtcNow)
                );
            return affected == 1;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var affected = await _context.Companies
                .Where(c => c.CompanyId == id)
                .ExecuteDeleteAsync();
            return affected == 1;
        }
    }
} 