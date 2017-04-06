using FaceBook.Data.Repository;
using FaceBook.Model;
using FaceBook.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBook.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userDetailRepo;

        public UserService(IRepository<User> userDetailRepo)
        {
            this._userDetailRepo = userDetailRepo;
        }


        public void AddInvitationForFriend(User logged, InvitationForFriend friend)
        {
            logged.InvitationForFriend.Add(friend);
            _userDetailRepo.SaveChanges();
        }

        public void ChangeOnline(User loggedUser)
        {
            if (loggedUser.IsOnline)
            {
                loggedUser.IsOnline = false;
                _userDetailRepo.SaveChanges();
            }
            else
            {
                loggedUser.IsOnline = true;
                _userDetailRepo.SaveChanges();
            }

        }


        public void AddNewFriend(User logged, User userAskForFriend)
        {
            //add friend both users
            logged.Friend.Add(userAskForFriend);
            userAskForFriend.Friend.Add(logged);

            //delete ask
            var askForFriend = logged.InvitationForFriend.Where(x => x.Username == userAskForFriend.UserName).FirstOrDefault();
            logged.InvitationForFriend.Remove(askForFriend);

            _userDetailRepo.SaveChanges();
        }


        public void RemoveInvitationForFriend(User logged, User userAskForFriend)
        {
            var askForFriend = logged.InvitationForFriend.Where(x => x.Username == userAskForFriend.UserName).FirstOrDefault();
            logged.InvitationForFriend.Remove(askForFriend);

            _userDetailRepo.SaveChanges();
        }

        //check friend for show button for add
        public bool CkeckForFriend(User logged, User friend)
        {
            User ckeckFriend = logged.Friend.Where(x => x.Id == friend.Id).FirstOrDefault();

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
            return this._userDetailRepo.All();
        }

        public User GetUserByUserName(string userName)
        {
            var allUser = this.GetAllUsers();

            return allUser.Where(x => x.UserName == userName).FirstOrDefault();
        }

    }
}
