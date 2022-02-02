using System.Threading.Tasks;
using DotNetAPI.DTOs;
using DotNetAPI.Interfaces;
using DotNetAPI.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace DotNetAPI.SignalR
{
    public class AlertHub : Hub
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IUserAccessor _userAccessor;
        public AlertHub(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public async Task SendMessage(AlertDto alertDto)
        {            
            var alert = await _alertRepository.CreateAlert(alertDto);
            
            await Clients.All.SendAsync("ReceiveAlert", alert.Value);
            
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var groupId = "AlertHub";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
            var alerts = await _alertRepository.GetAlerts();
            await Clients.Caller.SendAsync("LoadAlerts", alerts);
        }
    }
}