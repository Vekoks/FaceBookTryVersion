using FaceBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBook.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        User GetUserByUserName(string userName);

        bool CkeckForFriend(User logged, User friend);

        void AddFriend(User logged, User friend);

    }
}
