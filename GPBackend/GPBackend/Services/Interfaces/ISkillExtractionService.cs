using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPBackend.Services.Interfaces
{
    public interface ISkillExtractionService
    {
        Task<List<string>> ExtractSkillsAsync(byte[] resumeFile);
    }
} 