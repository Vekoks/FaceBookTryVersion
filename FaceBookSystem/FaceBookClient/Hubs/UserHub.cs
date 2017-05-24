using Microsoft.AspNet.SignalR;

namespace FaceBookClient.Hubs
{
    public class UserHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<UserHub>();
            context.Clients.All.displayStatus();
        }
    }
}