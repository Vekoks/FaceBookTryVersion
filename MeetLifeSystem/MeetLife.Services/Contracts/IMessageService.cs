using MeetLife.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLife.Services.Contracts
{
    public interface IMessageService
    {
        string DeletellNotificationForNoSeenMessageFromUser(string Sender, User UserLogged);

        void AddNewNoSeenMessage(User userLogged, string Message, User userReceiverMessage);

        List<StoreMessage> GetConversation(User userLogged, string UserConversation);
    }
}
