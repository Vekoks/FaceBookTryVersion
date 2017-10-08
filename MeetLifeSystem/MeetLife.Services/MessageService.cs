using MeetLife.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetLife.Model;
using MeetLife.Data.Repository;

namespace MeetLife.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<StoreMessage> _storeRepo;

        public MessageService(IRepository<User> userRepo, IRepository<StoreMessage> storeRepo)
        {
            this._userRepo = userRepo;
            this._storeRepo = storeRepo;
        }

        public void AddNewNoSeenMessage(User userLogged, string Message, User userReceiverMessage)
        {

            if (!userReceiverMessage.IsOnline)
            {
                //no see message
                userReceiverMessage.MissMessage.Add(new MissMessage
                {
                    UserName = userLogged.UserName
                });
                //

                _storeRepo.Add(new StoreMessage
                {
                    Sender = userLogged.UserName,
                    Letter = Message,
                    Receiver = userReceiverMessage.UserName,
                    Date = DateTime.Now,
                    Conversation = userLogged.UserName + "And" + userReceiverMessage.UserName
                });


            }
            else
            {
                _storeRepo.Add(new StoreMessage
                {
                    Sender = userLogged.UserName,
                    Letter = Message,
                    Receiver = userReceiverMessage.UserName,
                    Date = DateTime.Now,
                    Conversation = userLogged.UserName + "And" + userReceiverMessage.UserName
                });
            }

            _storeRepo.SaveChanges();
        }

        public string DeletellNotificationForNoSeenMessageFromUser(string Sender, User UserLogged)
        {
            var allNotification = UserLogged.MissMessage.Where(x => x.UserName == Sender).ToList();

            foreach (var notification in allNotification)
            {
                UserLogged.MissMessage.Remove(notification);
            }

            _userRepo.SaveChanges();

            return "secces";
        }

        public List<StoreMessage> GetConversation(User userLogged, string UserConversation)
        {
            var ConversationOne = _storeRepo.All().Where(x => x.Conversation == userLogged.UserName + "And" + UserConversation).ToList();
            var ConversationTwo = _storeRepo.All().Where(x => x.Conversation == UserConversation + "And" + userLogged.UserName).ToList();

            var combinedConversation = ConversationOne.Concat(ConversationTwo).OrderBy(x => x.Date).ToList();

            return combinedConversation;
        }
    }
}
