using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaceBookClient.Hubs
{
    public class NoSeenMessageHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NoSeenMessageHub>();
            context.Clients.All.displayAllNoSeenMessage();
        }
    }
}