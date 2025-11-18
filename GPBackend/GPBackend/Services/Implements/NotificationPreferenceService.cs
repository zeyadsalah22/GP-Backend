using AutoMapper;
using GPBackend.DTOs.Notification;
using GPBackend.Models;
using GPBackend.Models.Enums;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class NotificationPreferenceService : INotificationPreferenceService
    {
        private readonly INotificationPreferenceRepository _preferenceRepository;
        private readonly IMapper _mapper;
        public NotificationPreferenceService(
            INotificationPreferenceRepository preferenceRepository,
            IMapper mapper)
        {
            _preferenceRepository = preferenceRepository;
            _mapper = mapper;
        }

        public async Task<bool> DisableAllNotificationsAsync(int userId)
        {
            var NotificationPreferences = await _preferenceRepository.GetByUserIdAsync(userId);
            if(NotificationPreferences == null){
                return false;
            }

            NotificationPreferences.GloballyEnabled = false;
            NotificationPreferences.EnableReminders = false;
            NotificationPreferences.EnableSocial = false;
            NotificationPreferences.EnableSystem = false;
            bool result = await _preferenceRepository.UpdateAsync(NotificationPreferences);
            return result;
        }

        public async Task<bool> EnableAllNotificationsAsync(int userId)
        {
            var NotificationPreferences = await _preferenceRepository.GetByUserIdAsync(userId);
            if(NotificationPreferences == null){
                return false;
            }

            NotificationPreferences.GloballyEnabled = true;
            NotificationPreferences.EnableReminders = true;
            NotificationPreferences.EnableSocial = true;
            NotificationPreferences.EnableSystem = true;
            bool result = await _preferenceRepository.UpdateAsync(NotificationPreferences);
            return result;
        }

        public async Task<NotificationPreferenceResponseDto> GetUserPreferencesAsync(int userId)
        {
            var NotificationPreferences = await _preferenceRepository.GetOrCreateDefaultAsync(userId);

            var PreferenceDto = _mapper.Map<NotificationPreferenceResponseDto>(NotificationPreferences);
            return PreferenceDto;
        }

        public async Task<bool> ResetToDefaultAsync(int userId)
        {
            return await EnableAllNotificationsAsync(userId);
        }

        public async Task<bool> ShouldSendNotificationAsync(int userId, NotificationCategory notificationCategory)
        {
            var Preferences = await _preferenceRepository.GetByUserIdAsync(userId);
            if(Preferences == null)
                return true;
            
            if(!Preferences.GloballyEnabled)
                return false;

            return notificationCategory switch
            {
                NotificationCategory.Reminder => Preferences.EnableReminders,
                NotificationCategory.Social => Preferences.EnableSocial,
                NotificationCategory.System => Preferences.EnableSystem,
                _ => true,
            };
        }

        public async Task<NotificationPreferenceResponseDto> UpdateUserPreferencesAsync(int userId, NotificationPreferenceUpdateDto updateDto)
        {
            try{
                var Preferences = await _preferenceRepository.GetOrCreateDefaultAsync(userId);

                if(updateDto.UserId != userId)
                    return null;
                
                _mapper.Map(updateDto, Preferences);
                await _preferenceRepository.UpdateAsync(Preferences);

                return _mapper.Map<NotificationPreferenceResponseDto>(Preferences);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);

            }
        }
    }
}