using FaceBook.Data.Repository;
using FaceBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBook.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userDetailRepo;

        public UserService(IRepository<User> userDetailRepo)
        {
            this._userDetailRepo = userDetailRepo;
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
