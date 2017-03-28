using FaceBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBook.Services.Contracts
{
    public interface IUserDetailService
    {
        void AddDetails(UserDetails userDetails);

        UserDetails GetDetailByUserId(string UserId);

        IEnumerable<UserDetails> GetAllDetails();

        void UpdataDetail(UserDetails userDetails);
    }
}
