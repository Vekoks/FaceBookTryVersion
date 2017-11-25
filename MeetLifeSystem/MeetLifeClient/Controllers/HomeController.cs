using MeetLife.Model;
using MeetLife.Services.Contracts;
using MeetLifeClient.Models;
using MeetLifeClient.Models.HomeViewModels;
using MeetLifeClient.Models.ModelsForLiveInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MeetLifeClient.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IFrieandsInfo _infoAllUser;
        private readonly IAskFriendInfo _infoForAskFriend;
        private readonly INoSeenMessage _infoNoSeenMessage;
        private readonly ICommentOnThePost _infoCommentsOnThePost;
        private readonly INotificationOnUser _infoNotificationOnUser;

        public HomeController(IUserService userService,
                              IFrieandsInfo infoUser,
                              IAskFriendInfo infoFriend,
                              IAllPostInfo infoPost,
                              INoSeenMessage infoNoSeenMessage,
                              IMessageService messageService,
                              ICommentOnThePost infoCommentsOnThePost,
                              INotificationOnUser infoNotificationOnUser)
        {
            this._userService = userService;
            this._messageService = messageService;
            this._infoAllUser = infoUser;
            this._infoForAskFriend = infoFriend;
            this._infoNoSeenMessage = infoNoSeenMessage;
            this._infoCommentsOnThePost = infoCommentsOnThePost;
            this._infoNotificationOnUser = infoNotificationOnUser;
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

            var allAskForFriend = userLogged.InvitationForFriends.ToList();
            var message = userLogged.MissMessages.ToList();

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

        public ActionResult ConferFriend(string UserName, string confirm)
        {
            var loggedUser = _userService.GetUserByUserName(User.Identity.Name);

            var userAsk = _userService.GetUserByUserName(UserName);

            if (confirm.Contains("Accept"))
            {
                _userService.AddNewFriend(loggedUser, userAsk);
            }

            else if (confirm.Contains("Delete"))
            {
                _userService.RemoveInvitationForFriend(loggedUser, userAsk);
            }

            return RedirectToAction("Begining", "Home");
        }

        [HttpGet]
        public JsonResult ResultInfoForUsers()
        {
            var loggedUserName = this.User.Identity.Name;

            if (loggedUserName == "")
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var loggedUser = _userService.GetUserByUserName(loggedUserName);

            var result = _infoAllUser.GetFriends(loggedUser.Id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ResultInfoForAskFriend()
        {
            var loggedUser = _userService.GetUserByUserName(User.Identity.Name);

            if (loggedUser != null)
            {

                var result = _infoForAskFriend.GetDataForAskFriend(loggedUser.Id);

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AddNewNoSeenMessage(string UserName, string Message)
        {
            var userLogged = this._userService.GetUserByUserName(User.Identity.Name);

            var userReceiverMessage = this._userService.GetUserByUserName(UserName);

            _messageService.AddNewNoSeenMessage(userLogged, Message, userReceiverMessage);

            return Json(new { status = "Success", message = "Success" });
        }

        [HttpGet]
        public JsonResult GetAllNotificationForNoSeenMessage()
        {
            var userLogged = this._userService.GetUserByUserName(User.Identity.Name);

            var result = _infoNoSeenMessage.GetDataForMessage(userLogged.Id);

            var resultMessage = new List<HomeNoSeenMessageModel>();

            foreach (var message in result)
            {
                var isOnlineUser = this._userService.GetUserByUserName(message.FormUser).IsOnline;

                if (isOnlineUser)
                {
                    _messageService.DeletellNotificationForNoSeenMessageFromUser(message.FormUser, userLogged);
                }

                resultMessage.Add(new HomeNoSeenMessageModel
                {
                    FormUser = message.FormUser,
                    IsOnline = isOnlineUser
                });
            }

            return Json(resultMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletellNotificationForNoSeenMessage(string UserName)
        {
            var userLogged = this._userService.GetUserByUserName(this.User.Identity.Name);

            this._messageService.DeletellNotificationForNoSeenMessageFromUser(UserName, userLogged);

            return Json(new { status = "Success", message = "Success" });

        }

        [HttpPost]
        public JsonResult GetConversationWithUser(string UserName)
        {
            var userLogged = _userService.GetUserByUserName(this.User.Identity.Name);

            var conversation = _messageService.GetConversation(userLogged, UserName);

            return Json(conversation, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetNotificationsOnUser()
        {
            var userLogged = _userService.GetUserByUserName(this.User.Identity.Name);

            var resoultNotifications = _infoNotificationOnUser.GetDataForNotofiactionsOnUser(userLogged.Id).Reverse();

            return Json(resoultNotifications, JsonRequestBehavior.AllowGet);
        }
    }
}