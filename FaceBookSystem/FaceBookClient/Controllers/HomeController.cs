using FaceBook.Model;
using FaceBook.Services.Contracts;
using FaceBookClient.Models;
using FaceBookClient.Models.HomeViewModels;
using FaceBookClient.Models.ModelsForLiveInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FaceBookClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUsersInfo infoAllUser;
        private readonly IAskFriendInfo infoForAskFriend;
        private readonly IAllPostInfo infoForAllPost;
        private readonly INoSeenMessage infoNoSeenMessage;

        public HomeController(IUserService userService, IUsersInfo infoUser, IAskFriendInfo infoFriend, IAllPostInfo infoPost, INoSeenMessage infoNoSeenMessage)
        {
            this._userService = userService;
            this.infoAllUser = infoUser;
            this.infoForAskFriend = infoFriend;
            this.infoForAllPost = infoPost;
            this.infoNoSeenMessage = infoNoSeenMessage;
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
                    Messages = new List<MissMessage>(),
                    CountAskForFriend = 0
                };

                return View(notUserModel);
            }

            var allAskForFriend = userLogged.InvitationForFriend.ToList();
            var message = userLogged.MissMessage.ToList();

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
            var result = infoAllUser.GetData();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ResultInfoForAskFriend()
        {
            var loggedUser = _userService.GetUserByUserName(User.Identity.Name);

            if (loggedUser != null)
            {

                var result = infoForAskFriend.GetDataForAskFriend(loggedUser.Id);

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
            var result = infoForAllPost.GetDataAllPost();

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

        [HttpGet]
        public JsonResult GetAllNoSeenMessage()
        {
            var userLogged = this._userService.GetUserByUserName(User.Identity.Name);

            var result = infoNoSeenMessage.GetDataForMessage(userLogged.Id);

            var resultMessage = new List<HomeNoSeenMessageModel>();

            foreach (var message in result)
            {
                resultMessage.Add(new HomeNoSeenMessageModel
                {
                    FormUser = message.FormUser,
                    Message = message.Message
                });
            }

            return Json(resultMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllNotificationForNoSeenMessage()
        {
            var userLogged = this._userService.GetUserByUserName(User.Identity.Name);

            var result = infoNoSeenMessage.GetDataForMessage(userLogged.Id);

            var resultMessage = new List<string>();

            foreach (var message in result)
            {
                if (!resultMessage.Contains<string>(message.FormUser))
                {
                    resultMessage.Add(message.FormUser);
                }
            }

            return Json(resultMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeletellNotificationForNoSeenMessage(string UserName)
        {

            var userLogged = this._userService.GetUserByUserName(this.User.Identity.Name);

            this._userService.DeletellNotificationForNoSeenMessageFromUser(UserName, userLogged);

            return Json(new { status = "Success", message = "Success" });

        }
    }
}