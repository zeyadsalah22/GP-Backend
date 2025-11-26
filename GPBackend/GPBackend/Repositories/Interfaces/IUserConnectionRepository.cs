using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IUserConnectionRepository
    {
        /// <summary>
        /// Adds or updates a user's SignalR connection
        /// </summary>
        Task<UserConnection> AddOrUpdateConnectionAsync(int userId, string connectionId);

        /// <summary>
        /// Gets a user's connection by user ID
        /// </summary>
        Task<UserConnection?> GetConnectionByUserIdAsync(int userId);

        /// <summary>
        /// Gets a user's connection by connection ID
        /// </summary>
        Task<UserConnection?> GetConnectionByConnectionIdAsync(string connectionId);

        /// <summary>
        /// Removes a user's connection
        /// </summary>
        Task<bool> RemoveConnectionAsync(int userId, string connectionId);

        /// <summary>
        /// Removes a connection by connection ID
        /// </summary>
        Task<bool> RemoveConnectionByConnectionIdAsync(string connectionId);

        /// <summary>
        /// Updates the last activity timestamp for a user's connection
        /// </summary>
        Task<bool> UpdateLastActivityAsync(int userId);

        /// <summary>
        /// Gets all active connections
        /// </summary>
        Task<List<UserConnection>> GetAllActiveConnectionsAsync();

        /// <summary>
        /// Checks if a user is currently connected
        /// </summary>
        Task<bool> IsUserConnectedAsync(int userId);

        /// <summary>
        /// Gets connection IDs for multiple users
        /// </summary>
        Task<List<string>> GetConnectionIdsByUserIdsAsync(List<int> userIds);
    }
}
