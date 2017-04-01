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

        public void AddFriend(User logged, User friend)
        {
            logged.Friend.Add(friend);
            _userDetailRepo.SaveChanges();
        }

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
