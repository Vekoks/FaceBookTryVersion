using FaceBook.Data;
using FaceBook.Model;
using FaceBook.Services.Contracts;
using FaceBookClient.Hubs;
using FaceBookClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FaceBookClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private UsersInfo info = new UsersInfo();
        private AskFriendInfo infoFriend = new AskFriendInfo();
        private AllPostInf infoPost = new AllPostInf();

        public HomeController(IUserService userService)
        {
            this._userService = userService;
        }

        public ActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Begining", "Home");
            }

            return View();
        }

        public ActionResult Begining()
        {
            var name = this.User.Identity.Name;
            var userLogged = _userService.GetUserByUserName(name);

            if (userLogged == null)
            {
                var notUserModel = new HomeIndexViewModel()
                {
                    AllAskForFriend = new List<InvitationForFriend>(),
                    Messages = new List<Message>(),
                    CountAskForFriend = 0
                };

                return View(notUserModel);
            }

            var allAskForFriend = userLogged.InvitationForFriend.ToList();
            var message = userLogged.Message.ToList();

            var model = new HomeIndexViewModel()
            {
                AllAskForFriend = allAskForFriend,
                Messages = message,
                CountAskForFriend = allAskForFriend.Count
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult ConferFriend(string UserName, string confer)
        {
            var loggedUser = _userService.GetUserByUserName(User.Identity.Name);

            var userAsk = _userService.GetUserByUserName(UserName);

            if (confer.Contains("accept"))
            {
                _userService.AddNewFriend(loggedUser, userAsk);
            }

            else if (confer.Contains("delete"))
            {
                _userService.RemoveInvitationForFriend(loggedUser, userAsk);
            }

            return RedirectToAction("Begining", "Home");
        }

        [HttpGet]
        public JsonResult ResultInfoForUsers()
        {
            var result = info.GetData();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ResultInfoForAskFriend()
        {
            var loggedUser = _userService.GetUserByUserName(User.Identity.Name);

            if (loggedUser != null)
            {

                var result = infoFriend.GetData(loggedUser.Id);

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetAllPostFromUsers()
        {
            var result = infoPost.GetDataAllPost();

            var resultPost = new List<HomePostModel>();

            foreach (var post in result)
            {
                resultPost.Add(new HomePostModel
                {
                    UserName = _userService.GetUserById(post.UserId).UserName,
                    DiscriptionPost = post.Discription,
                    DateOnPost = (int)DateTime.Now.Subtract(post.DatePost).TotalMinutes
            });
            }

            var resultForView = resultPost.OrderBy(x => x.DateOnPost).ToList();

            return Json(resultForView, JsonRequestBehavior.AllowGet);
        }
    }
}