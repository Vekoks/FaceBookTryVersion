using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaceBookClient.Hubs
{
    public class LikeHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<LikeHub>();
            context.Clients.All.displayLikes();
        }
    }
}