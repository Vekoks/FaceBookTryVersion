﻿using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Hubs
{
    public class PostHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PostHub>();
            context.Clients.All.displayPost();
        }
    }
}