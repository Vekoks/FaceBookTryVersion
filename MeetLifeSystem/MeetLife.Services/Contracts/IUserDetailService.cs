using MeetLife.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MeetLife.Services.Contracts
{
    public interface IUserDetailService
    {
        void AddDetails(UserDetails UserDetails);

        UserDetails GetDetailByUserId(string UserId);

        IEnumerable<UserDetails> GetAllDetails();

        void UpdataDetail(UserDetails UserDetails);

        Picture AddNewPictureOnUser(UserDetails UserDetails, string Description, byte[] Picture);

        void AddNewProfilePicture(UserDetails UserDetails, byte[] Picture);

        IEnumerable<Picture> GetAllPisturesOnUser(UserDetails UserDetails);

        Picture GetProfilePicture(UserDetails UserDetails);

        void ChangeProfilePicture(UserDetails UserDetails, int NewPictureId);
    }
}
