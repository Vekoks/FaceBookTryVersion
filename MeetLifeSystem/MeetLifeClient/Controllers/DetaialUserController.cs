using MeetLife.Data;
using MeetLife.Data.Repository;
using MeetLife.Model;
using MeetLife.Services;
using MeetLife.Services.Contracts;
using MeetLifeClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetLifeClient.Controllers
{
    public class DetaialUserController : Controller
    {
        private readonly IUserDetailService _detailService;
        private readonly IUserService _userService;

        public DetaialUserController(IUserService userService, IUserDetailService detailService)
        {
            this._detailService = detailService;
            this._userService = userService;
        }

        // GET: DetaialUser
        public ActionResult Index()
        {
            var name = User.Identity.Name;

            var userLogged = _userService.GetUserByUserName(name);

            var details = _detailService.GetDetailByUserId(userLogged.Id);

            var profilPicture = _detailService.GetProfilePicture(details);

            if (details == null)
            {
                var model = new UserDetailsViewModel()
                {
                    FirstName = "undefined",
                    LastName = "undefined",
                    Adress = "undefined",
                    Age = 0,
                    ImageBrand = "",
                    Friends = userLogged.Friends,
                    Post = userLogged.Posts.OrderByDescending(x => x.DateOnPost)
                };

                return View(model);
            }
            else
            {

                var model = new UserDetailsViewModel()
                {
                    FirstName = details.FirstName,
                    LastName = details.LastName,
                    Adress = details.Adress,
                    Age = details.Age,
                    ImageBrand = this.ConvertByteArrToStringForImg(profilPicture.Image),
                    Friends = userLogged.Friends,
                    Post = userLogged.Posts.OrderByDescending(x => x.DateOnPost)
                };

                return View(model);
            }
        }

        // GET: DetaialUser/Details/UserName
        public ActionResult Details(string UserName)
        {
            var name = User.Identity.Name;

            var userLogged = _userService.GetUserByUserName(name);

            var userFriend = _userService.GetUserByUserName(UserName);

            var details = _detailService.GetDetailByUserId(userFriend.Id);
            var profilPicture = _detailService.GetProfilePicture(details);

            var ckeckFriend = _userService.CkeckForFriend(userLogged, userFriend);

            var model = new FriendDetailViewModel();


            if (details == null)
            {
                model.UserName = userFriend.UserName;
                model.FirstName = "undefined";
                model.LastName = "undefined";
                model.Adress = "undefined";
                model.Age = 0;
                model.ImageUser = "";
                model.CheckForFriend = ckeckFriend;
            }
            else
            {
                model.UserName = userFriend.UserName;
                model.FirstName = details.FirstName;
                model.LastName = details.LastName;
                model.Adress = details.Adress;
                model.Age = details.Age;
                model.ImageUser = this.ConvertByteArrToStringForImg(profilPicture.Image);
                model.CheckForFriend = ckeckFriend;
            }


            return View("FriendDetailsView", model);
        }

        public ActionResult Pictures(string UserName)
        {
            var user = _userService.GetUserByUserName(UserName);

            var userDetail = _detailService.GetDetailByUserId(user.Id);

            var allPictures = _detailService.GetAllPisturesOnUser(userDetail);

            var listeInfoPuctires = new List<InfoPuctires>();

            foreach (var picture in allPictures)
            {
                listeInfoPuctires.Add(new InfoPuctires
                {
                    PictureId = picture.Id,
                    Destriction = picture.Description,
                    Date = (int)DateTime.Now.Subtract(picture.DateUploading).TotalMinutes,
                    SrcPistures = this.ConvertByteArrToStringForImg(picture.Image),
                    IsProfilePicture = picture.IsProfilPicture
                });
            }

            var model = new UserPicturesModel
            {
                UserName = UserName,
                SrcPistures = listeInfoPuctires
            };

            return View(model);
        }

        public ActionResult PicturesSave(string discriptin, HttpPostedFileBase image)
        {
            var name = User.Identity.Name;

            var user = _userService.GetUserByUserName(name);

            var userDetail = _detailService.GetDetailByUserId(user.Id);

            var picutre = new byte[image.ContentLength];
            image.InputStream.Read(picutre, 0, image.ContentLength);

            _detailService.AddNewPictureOnUser(userDetail, discriptin, picutre);

            return RedirectToAction("Pictures/" + User.Identity.Name, "DetaialUser");
        }

        // POST: DetaialUser/Create
        [HttpPost]
        public ActionResult Create(UserDetailsViewModel model, HttpPostedFileBase image)
        {
            var name = User.Identity.Name;

            var user = _userService.GetUserByUserName(name);

            var userDetail = _detailService.GetDetailByUserId(user.Id);

            var pitureBytes = new byte[image.ContentLength];
            image.InputStream.Read(pitureBytes, 0, image.ContentLength);

            if (userDetail != null)
            {
                userDetail.FirstName = model.FirstName;
                userDetail.LastName = model.LastName;
                userDetail.Adress = model.Adress;
                userDetail.Age = model.Age;

                _detailService.UpdataDetail(userDetail);
                _detailService.AddNewProfilePicture(userDetail, pitureBytes);
            }
            else
            {

                var newDetails = new UserDetails()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Adress = model.Adress,
                    Age = model.Age,
                    UserId = user.Id,
                };

                _detailService.AddDetails(newDetails);
                _detailService.AddNewProfilePicture(newDetails, pitureBytes);
            }

            return RedirectToAction("Index", "DetaialUser");
        }

        //add invitation for friend
        public ActionResult AddInvitationFriend(string UserName)
        {
            var name = User.Identity.Name;

            var userLogged = _userService.GetUserByUserName(name);

            var userFriend = _userService.GetUserByUserName(UserName);

            var newAskForFriend = new InvitationForFriend()
            {
                Username = userLogged.UserName
            };

            _userService.AddInvitationForFriend(userFriend, newAskForFriend);

            return RedirectToAction("Begining", "Home");
        }

        public ActionResult ChangeProfilePicture(string PictureId)
        {
            var name = User.Identity.Name;

            var user = _userService.GetUserByUserName(name);

            var userDetail = _detailService.GetDetailByUserId(user.Id);

            _detailService.ChangeProfilePicture(userDetail, int.Parse(PictureId));

            return Json(new { status = "Success", message = name });
        }

        public string ConvertByteArrToStringForImg(byte[] arr)
        {
            var base64 = Convert.ToBase64String(arr);
            var srcImg = string.Format("data:image/gif;base64,{0}", base64);

            return srcImg;
        }
    }
}
