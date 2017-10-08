using Microsoft.AspNet.SignalR;

namespace MeetLifeClient.Hubs
{
    public class UserHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<UserHub>();
            context.Clients.All.displayUser();
        }
    }
}