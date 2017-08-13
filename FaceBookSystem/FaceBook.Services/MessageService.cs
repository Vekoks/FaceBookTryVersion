using FaceBook.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceBook.Model;
using FaceBook.Data.Repository;

namespace FaceBook.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<User> _userRepo;

        public MessageService(IRepository<User> userRepo)
        {
            this._userRepo = userRepo;
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

                userLogged.StoreMessage.Add(new StoreMessage
                {
                    Receiver = userLogged.UserName,
                    Letter = Message,
                    Date = DateTime.Now
                });

            }
            else
            {
                userLogged.StoreMessage.Add(new StoreMessage
                {
                    Receiver = userLogged.UserName,
                    Letter = Message,
                    Date = DateTime.Now
                });
            }

            _userRepo.SaveChanges();
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
    }
}
