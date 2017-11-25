using MeetLife.Data.Repository;
using MeetLife.Model;
using MeetLife.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLife.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepo;

        public UserService(IRepository<User> userRepo)
        {
            this._userRepo = userRepo;
        }


        public void AddInvitationForFriend(User logged, InvitationForFriend friend)
        {
            logged.InvitationForFriends.Add(friend);
            _userRepo.SaveChanges();
        }

        public void ChangeOnline(User loggedUser)
        {
            if (loggedUser.IsOnline)
            {
                loggedUser.IsOnline = false;
                _userRepo.SaveChanges();
            }
            else
            {
                loggedUser.IsOnline = true;
                _userRepo.SaveChanges();
            }

        }

        public void AddNewFriend(User logged, User userAskForFriend)
        {
            //add friend both users
            logged.Friends.Add(userAskForFriend);
            userAskForFriend.Friends.Add(logged);

            //delete ask
            var askForFriend = logged.InvitationForFriends.Where(x => x.Username == userAskForFriend.UserName).FirstOrDefault();
            logged.InvitationForFriends.Remove(askForFriend);

            _userRepo.SaveChanges();
        }


        public void RemoveInvitationForFriend(User logged, User userAskForFriend)
        {
            var askForFriend = logged.InvitationForFriends.Where(x => x.Username == userAskForFriend.UserName).FirstOrDefault();
            logged.InvitationForFriends.Remove(askForFriend);

            _userRepo.SaveChanges();
        }

        //check friend for show button for add
        public bool CkeckForFriend(User logged, User friend)
        {
            User ckeckFriend = logged.Friends.Where(x => x.Id == friend.Id).FirstOrDefault();

            try
            {
                var name = ckeckFriend.UserName;
            }
            catch (NullReferenceException e)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this._userRepo.All();
        }

        public User GetUserByUserName(string userName)
        {
            var allUser = this.GetAllUsers();

            return allUser.Where(x => x.UserName == userName).FirstOrDefault();
        }

        public User GetUserById(string id)
        {
            var userFind = this.GetAllUsers().Where(x => x.Id == id).FirstOrDefault();

            return userFind;
        }
    }
}
