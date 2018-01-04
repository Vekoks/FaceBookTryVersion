using MeetLife.Model;
using MeetLife.Services.Contracts;
using MeetLifeClient.Models;
using MeetLifeClient.Models.HomeViewModels;
using MeetLifeClient.Models.ModelsForLiveInfo;
using MeetLifeClient.Models.ModelsForLiveInfo.Contracts;
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
        private readonly IUsersInfo _infoAllUser;
        private readonly IAskFriendInfo _infoForAskFriend;
        private readonly INoSeenMessage _infoNoSeenMessage;
        private readonly ICommentOnThePost _infoCommentsOnThePost;
        private readonly INotificationOnUser _infoNotificationOnUser;
        private readonly IPostService _postService;

        public HomeController(IUserService userService,
                              IUsersInfo infoUser,
                              IAskFriendInfo infoFriend,
                              IAllPostInfo infoPost,
                              INoSeenMessage infoNoSeenMessage,
                              IMessageService messageService,
                              ICommentOnThePost infoCommentsOnThePost,
                              INotificationOnUser infoNotificationOnUser,
                              IPostService postService)
        {
            this._userService = userService;
            this._messageService = messageService;
            this._infoAllUser = infoUser;
            this._infoForAskFriend = infoFriend;
            this._infoNoSeenMessage = infoNoSeenMessage;
            this._infoCommentsOnThePost = infoCommentsOnThePost;
            this._infoNotificationOnUser = infoNotificationOnUser;
            this._postService = postService;
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
                    CountAskForFriend = 0,
                    Posts = new List<HomePostModel>()
            };

                return View(notUserModel);
            }

            //Posts
            var result = _postService.GetAllPost().OrderByDescending(x=>x.DateOnPost);

            var resultPost = new List<HomePostModel>();

            foreach (var post in result)
            {
                var timeForShowPosts = (DateTime.Now - post.DateOnPost);

                if (timeForShowPosts.Minutes < 10 || timeForShowPosts.Hours > 0)
                {
                    continue;
                }

                var commentsPost = new List<ViewModelComment>();
                var commentList = post.Comments;

                foreach (var comment in commentList)
                {
                    var profilePicture = _postService.GetPictureProfileFromPost(_userService.GetUserById(post.UserId));

                    commentsPost.Add(new ViewModelComment()
                    {
                        Username = comment.Username,
                        Description = comment.Description,
                        PictureProfile = Converts.ConvertByteArrToStringForImg(profilePicture)
                    });
                }

                var likesPost = new List<ViewModelLike>();
                var likeList = post.Likes;

                foreach (var like in likeList)
                {
                    var pictureOfProfile = _postService.GetPictureProfileFromPost(_userService.GetUserById(post.UserId));

                    likesPost.Add(new ViewModelLike()
                    {
                        Username = like.Username,
                        PictureProfile = Converts.ConvertByteArrToStringForImg(pictureOfProfile)
                    });
                }

                var pictureId = 0;
                int.TryParse(post.PictureId.ToString(), out pictureId);

                var pictureOfUser = _postService.GetPictureProfileFromPost(_userService.GetUserById(_userService.GetUserById(post.UserId).Id));

                resultPost.Add(new HomePostModel
                {
                    PostId = post.Id,
                    UserName = _userService.GetUserById(post.UserId).UserName,
                    PictureOfUser = Converts.ConvertByteArrToStringForImg(pictureOfUser),
                    DiscriptionPost = post.Disctription,
                    PicturePost = Converts.ConvertByteArrToStringForImg(_postService.GetPictureOnPost(pictureId)),
                    DateOnPost = Converts.CreateStringDate(post.DateOnPost),
                    Likes = likesPost,
                    Comments = commentsPost
                });
            }


            var allAskForFriend = userLogged.InvitationForFriends.ToList();
            var message = userLogged.MissMessages.ToList();

            var model = new HomeIndexViewModel()
            {
                AllAskForFriend = allAskForFriend,
                Messages = message,
                CountAskForFriend = allAskForFriend.Count,
                Posts = resultPost
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

            return Json(new { status = "Success", message = "Success" });
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

            var result = _infoAllUser.GetData(loggedUser.Id);

            var listFriend = new List<HomeFriendModel>();

            foreach (var friend in result)
            {
                var pictureOfProfile = _postService.GetPictureProfileFromPost(_userService.GetUserByUserName(friend.Name));

                listFriend.Add(new HomeFriendModel()
                {
                    Name = friend.Name,
                    IsOnline = friend.IsOnline,
                    ProfilPicture = Converts.ConvertByteArrToStringForImg(pictureOfProfile),
                });
            }


            return Json(listFriend, JsonRequestBehavior.AllowGet);
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

            var conversationModel = new List<HomeChatModel>();

            foreach (var part in conversation)
            {
                var pictureOfProfile = _postService.GetPictureProfileFromPost(_userService.GetUserByUserName(part.Sender));

                if (part.Sender.Contains(this.User.Identity.Name))
                {
                    conversationModel.Add(new HomeChatModel()
                    {
                        IsMe = true,
                        UserName = part.Sender,
                        Picture = Converts.ConvertByteArrToStringForImg(pictureOfProfile),
                        Letter = part.Letter,
                        Date = Converts.CreateStringDate(part.Date),
                    });
                }
                else
                {
                    conversationModel.Add(new HomeChatModel()
                    {
                        IsMe = false,
                        UserName = part.Sender,
                        Picture = Converts.ConvertByteArrToStringForImg(pictureOfProfile),
                        Letter = part.Letter,
                        Date = Converts.CreateStringDate(part.Date),
                    });
                }
            }

            return Json(conversationModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetNotificationsOnUser()
        {
            var userLogged = _userService.GetUserByUserName(this.User.Identity.Name);

            var resoultNotifications = _infoNotificationOnUser.GetDataForNotofiactionsOnUser(userLogged.Id).Reverse();

            var countNotifications = resoultNotifications.Where(x => x.IsSaw == false).Count();

            var listNotification = new List<ListNotification>();

            foreach (var notification in resoultNotifications)
            {
                var pictureOfProfile = _postService.GetPictureProfileFromPost(_userService.GetUserByUserName(notification.Username));

                listNotification.Add(new ListNotification
                {
                    Username = notification.Username,
                    Description = notification.Description,
                    ImgUser = Converts.ConvertByteArrToStringForImg(pictureOfProfile),
                    NotificationId = notification.NotificationId,
                    IsSaw = notification.IsSaw,
                    PostId = notification.PostId
            });
            }

            var resoultModel = new HomeNotificationModel()
            {
                CountNoSeenNotification = countNotifications,
                Notification = listNotification
            };

            return Json(resoultModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SearchUsers()
        {
            var searchUsers = _userService.GetAllUsers().ToList();

            var resoult = new List<HomeSearchUserModel>();

            foreach (var user in searchUsers)
            {
                var profilePicture = user.Posts.Where(x => x.IsProfilePicture == true).FirstOrDefault().Picture;

                resoult.Add(new HomeSearchUserModel()
                {
                    UserName = user.UserName,
                    ProfilePicture = Converts.ConvertByteArrToStringForImg(profilePicture.Image)
                });
            }

            return Json(resoult, JsonRequestBehavior.AllowGet);
        }
    }
}