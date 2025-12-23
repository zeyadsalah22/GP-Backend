using GPBackend.DTOs.Application;
using GPBackend.DTOs.Common;
using GPBackend.Models.Enums;

namespace GPBackend.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<ApplicationResponseDto?> GetApplicationByIdAsync(int id, int userId);
        Task<PagedResult<ApplicationResponseDto>> GetFilteredApplicationsAsync(int userId, ApplicationQueryDto queryDto);
        Task<IEnumerable<ApplicationResponseDto>> GetAllApplicationsByUserIdAsync(int userId);
        Task<ApplicationResponseDto> CreateApplicationAsync(int userId, ApplicationCreateDto createDto);
        Task<bool> UpdateApplicationAsync(int id, int userId, ApplicationUpdateDto updateDto);
        Task<bool> DeleteApplicationAsync(int id, int userId);
        Task<bool> ApplicationExistsAsync(int id);
        Task<int> BulkDeleteApplicationsAsync(IEnumerable<int> ids, int userId);
        Task<bool> RecordStageAsync(int applicationId, int userId, ApplicationStage stage, DateOnly date, string? note = null);
    }
} 