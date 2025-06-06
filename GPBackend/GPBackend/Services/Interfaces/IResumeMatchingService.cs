using GPBackend.DTOs.ResumeMatching;
using System.Threading.Tasks;

namespace GPBackend.Services.Interfaces
{
    public interface IResumeMatchingService
    {
        Task<ResumeMatchingResponseDto> MatchResumeAsync(ResumeMatchingRequestDto request);
    }
} 