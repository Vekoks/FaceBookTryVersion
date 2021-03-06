﻿using MeetLife.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetLife.Model;
using MeetLife.Data.Repository;
using MeetLife.Data.UnitToWork;

namespace MeetLife.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<StoreMessage> _storeRepo;
        private readonly IUnitToWorkDbContext _iUnitToWorkDbContext;

        public MessageService(IRepository<User> userRepo, IRepository<StoreMessage> storeRepo, IUnitToWorkDbContext iUnitToWorkDbContext)
        {
            this._userRepo = userRepo;
            this._storeRepo = storeRepo;
            this._iUnitToWorkDbContext = iUnitToWorkDbContext;
        }

        public void AddNewNoSeenMessage(User userLogged, string Message, User userReceiverMessage)
        {
            //no see message
            userReceiverMessage.MissMessages.Add(new MissMessage
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

            //_storeRepo.SaveChanges();
            _iUnitToWorkDbContext.Commit();
        }

        public string DeletellNotificationForNoSeenMessageFromUser(string Sender, User UserLogged)
        {
            var allNotification = UserLogged.MissMessages.Where(x => x.UserName == Sender).ToList();

            foreach (var notification in allNotification)
            {
                UserLogged.MissMessages.Remove(notification);
            }

            //_userRepo.SaveChanges();
            _iUnitToWorkDbContext.Commit();

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
