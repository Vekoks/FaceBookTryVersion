using MeetLife.Data.Repository;
using MeetLife.Data.UnitToWork;
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
        private readonly IUnitToWorkDbContext _iUnitToWorkDbContext;

        public  UserDetailService(IRepository<UserDetails> userDetailRepo, IUnitToWorkDbContext iUnitToWorkDbContext)
        {
            this._userDetailRepo = userDetailRepo;
            this._iUnitToWorkDbContext = iUnitToWorkDbContext;
        }

        public void AddDetails(UserDetails UserDetails)
        {
            this._userDetailRepo.Add(UserDetails);
            //this._userDetailRepo.SaveChanges();
            _iUnitToWorkDbContext.Commit();
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
            //this._userDetailRepo.SaveChanges();
            _iUnitToWorkDbContext.Commit();
        }

        //public void AddNewPictureOnUser(UserDetails UserDetails, string Description, byte[] Picture)
        //{
        //    UserDetails.Pitures.Add(new Picture
        //    {
        //        Description = Description,
        //        DateUploading = DateTime.Now,
        //        Image = Picture,
        //        IsProfilPicture = false
        //    });

        //    _userDetailRepo.SaveChanges();
        //}

        //public void AddNewProfilePicture(UserDetails UserDetails, byte[] Picture)
        //{
        //    var oldProfilPicture = UserDetails.Pitures.Where(x => x.IsProfilPicture == true).FirstOrDefault();

        //    if (oldProfilPicture != null)
        //    {
        //        oldProfilPicture.IsProfilPicture = false;
        //    }

        //    UserDetails.Pitures.Add(new Picture
        //    {
        //        Description = "",
        //        DateUploading = DateTime.Now,
        //        Image = Picture,
        //        IsProfilPicture = true
        //    });

        //    _userDetailRepo.SaveChanges();
        //}

        //public IEnumerable<Picture> GetAllPisturesOnUser(UserDetails UserDetails)
        //{
        //    var resoult = new List<Picture>();

        //    try
        //    {
        //        resoult = UserDetails.Pitures.OrderBy(x=>x.DateUploading).ToList();
        //    }
        //    catch (Exception)
        //    {
               
        //    }

        //    return resoult;
        //}

        //public Picture GetProfilePicture(UserDetails UserDetails)
        //{
        //    var resoult = new Picture();

        //    try
        //    {
        //        resoult = UserDetails.Pitures.Where(x=>x.IsProfilPicture == true).FirstOrDefault();
        //    }
        //    catch (Exception)
        //    {

        //    }

        //    return resoult;
        //}

        public void ChangeProfilePicture(UserDetails UserDetails, int NewPictureId)
        {
            //var oldProfilePicture = UserDetails.Pitures.Where(x => x.IsProfilPicture == true).FirstOrDefault();
            //oldProfilePicture.IsProfilPicture = false;

            //var newProfilePicture = UserDetails.Pitures.Where(x => x.Id == NewPictureId).FirstOrDefault();
            //newProfilePicture.IsProfilPicture = true;

            //_userDetailRepo.SaveChanges();
        }
    }
}
