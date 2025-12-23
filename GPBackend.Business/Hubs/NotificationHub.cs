using System.Security.Claims;
using System.Threading.Tasks;
using GPBackend.hubs;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace GPBackend.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IUserConnectionRepository _userConnectionRepo;

        public NotificationHub(IUserConnectionRepository UserConnectionRepo)
        {
            _userConnectionRepo = UserConnectionRepo;
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            string ConnectionID = Context.ConnectionId;

            Claim? claim = Context.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.NameIdentifier);

            if(claim == null)
            {
                return;
            }

            var userConnection = await _userConnectionRepo.AddOrUpdateConnectionAsync(int.Parse(claim.Value), ConnectionID);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);

            string ConnectionID = Context.ConnectionId;

            bool result = await _userConnectionRepo.RemoveConnectionByConnectionIdAsync(ConnectionID);
            if (!result)
            {
                throw new Exception(message: "An error occurred while removing the connectionID in OnDisconnectedAsync");
            }
        }
    }
}