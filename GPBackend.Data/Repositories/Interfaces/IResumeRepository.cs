using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IResumeRepository
    {
        Task<IEnumerable<Resume>> GetAllAsync(int userId);
        Task<Resume?> GetByIdAsync(int id);
        Task<Resume> CreateAsync(Resume resume);
        Task<bool> UpdateAsync(Resume resume);
        Task<bool> DeleteAsync(int id);
    }
}
