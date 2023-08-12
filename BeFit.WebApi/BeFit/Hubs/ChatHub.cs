using Microsoft.AspNetCore.SignalR;
using static BeFit.Common.GeneralApplicationConstants;

namespace BeFit.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await this.Clients.All.SendAsync(SignalrRemoteMethod, user, message);
        }

    }
}
