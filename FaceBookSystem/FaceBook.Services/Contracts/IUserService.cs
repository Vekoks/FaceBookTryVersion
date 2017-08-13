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

        User GetUserById(string id);

        bool CkeckForFriend(User logged, User friend);

        void AddInvitationForFriend(User logged, InvitationForFriend friend);

        void AddNewFriend(User logged, User userAskForFriend);

        void RemoveInvitationForFriend(User logged, User userAskForFriend);

        void ChangeOnline(User loggedUser);

    }
}
