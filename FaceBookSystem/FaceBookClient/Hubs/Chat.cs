using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using FaceBook.Services.Contracts;
using System.Web.Security;

namespace FaceBookClient.Hubs
{
    public class Chat : Hub
    {
        public void SendMessage(string username, string message)
        {
            var msg = string.Format("{0}: {1}", Context.User.Identity.Name, message);
            //Clients.All.addMessage(msg);
            //Clients.Group(Context.User.Identity.Name).addMessage(message);

            Clients.User(Context.User.Identity.Name).addMessage(msg);
            Clients.User(username).addMessage(msg);

        }

        public void JoinRoom(string room)
        {
            Groups.Add(Context.ConnectionId, room);
            Clients.Caller.joinRoom(room);
        }

        public void SendMessageToRoom(string message, string[] rooms)
        {
            var msg = string.Format("{0}: {1}", Context.ConnectionId, message);

            for (int i = 0; i < rooms.Length; i++)
            {
                Clients.Group(rooms[i]).addMessage(msg);
            }
        }
    }
}