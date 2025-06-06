using System.Threading.Tasks;

namespace GPBackend.Services.Interfaces
{
    public interface ISkillMatchingApiClient
    {
        Task<double> GetMatchingScoreAsync(string skills, string jobDescription);
    }
} 