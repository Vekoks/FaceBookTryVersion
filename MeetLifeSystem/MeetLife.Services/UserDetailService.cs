using MeetLife.Data.Repository;
using MeetLife.Model;
using MeetLife.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MeetLife.Services
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IRepository<UserDetails> _userDetailRepo;

        public  UserDetailService(IRepository<UserDetails> userDetailRepo)
        {
            this._userDetailRepo = userDetailRepo;
        }

        public void AddDetails(UserDetails UserDetails)
        {
            this._userDetailRepo.Add(UserDetails);
            this._userDetailRepo.SaveChanges();
        }

        public UserDetails GetDetailByUserId(string UserId)
        {
            var allDetails = this.GetAllDetails();

            return allDetails.Where(x => x.UserId == UserId).FirstOrDefault();
        }

        public IEnumerable<UserDetails> GetAllDetails()
        {
            return _userDetailRepo.All();
        }

        public void UpdataDetail(UserDetails UserDetails)
        {
            this._userDetailRepo.Update(UserDetails);
            this._userDetailRepo.SaveChanges();
        }

        public void AddNewPictureOnUser(UserDetails UserDetails, string Description, byte[] Picture)
        {
            UserDetails.Pitures.Add(new Picture
            {
                Description = Description,
                DateUploading = DateTime.Now,
                Image = Picture
            });

            _userDetailRepo.SaveChanges();
        }

        public IEnumerable<Picture> GetAllPisturesOnUser(UserDetails UserDetails)
        {
            var resoult = new List<Picture>();

            try
            {
                resoult = UserDetails.Pitures.OrderBy(x=>x.DateUploading).ToList();
            }
            catch (Exception)
            {
               
            }

            return resoult;
        }
    }
}
