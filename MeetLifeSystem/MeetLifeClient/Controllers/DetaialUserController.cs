using MeetLife.Data;
using MeetLife.Data.Repository;
using MeetLife.Model;
using MeetLife.Services;
using MeetLife.Services.Contracts;
using MeetLifeClient.Models;
using MeetLifeClient.Models.DetailsViewModels;
using MeetLifeClient.Models.HomeViewModels;
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
        private readonly IPostService _postService;

        public DetaialUserController(IUserService userService, IUserDetailService detailService, IPostService postService)
        {
            this._detailService = detailService;
            this._userService = userService;
            this._postService = postService;
        }

        // GET: DetaialUser
        public ActionResult Index()
        {
            var name = User.Identity.Name;

            var userLogged = _userService.GetUserByUserName(name);

            var details = _detailService.GetDetailByUserId(userLogged.Id);

            var profilPicture = _postService.GetPictureProfileFromPost(userLogged);

            var listPost = userLogged.Posts.OrderByDescending(x => x.DateOnPost);

            var viewListPost = new List<HomePostModel>();

            foreach (var post in listPost)
            {
                var pictureId = 0;
                int.TryParse(post.PictureId.ToString(), out pictureId);

                var commentsPost = new List<ViewModelComment>();

                foreach (var comment in post.Comments)
                {
                    var profilePicture = _postService.GetPictureProfileFromPost(_userService.GetUserByUserName(comment.Username));

                    commentsPost.Add(new ViewModelComment()
                    {
                        Username = comment.Username,
                        Description = comment.Description,
                        PictureProfile = Converts.ConvertByteArrToStringForImg(profilePicture)
                    });
                }

                var likesPost = new List<ViewModelLike>();

                foreach (var like in post.Likes)
                {
                    var pictureOfProfile = _postService.GetPictureProfileFromPost(_userService.GetUserByUserName(like.Username));

                    likesPost.Add(new ViewModelLike()
                    {
                        Username = like.Username,
                        PictureProfile = Converts.ConvertByteArrToStringForImg(pictureOfProfile)
                    });
                }

                var pictureOfUser = _postService.GetPictureProfileFromPost(_userService.GetUserById(post.UserId));

                viewListPost.Add(new HomePostModel
                {
                    PostId = post.Id,
                    UserName = _userService.GetUserById(post.UserId).UserName,
                    PictureOfUser = Converts.ConvertByteArrToStringForImg(pictureOfUser),
                    DiscriptionPost = post.Disctription,
                    DateOnPost = Converts.CreateStringDate(post.DateOnPost),
                    PicturePost = Converts.ConvertByteArrToStringForImg(_postService.GetPictureOnPost(pictureId)),
                    Likes = likesPost,
                    Comments = commentsPost
                });
            }

            var friendUser = userLogged.Friends;
            var listFriend = new List<FriendViewModel>();

            foreach (var friend in friendUser)
            {
                var pictureOfUser = _postService.GetPictureProfileFromPost(_userService.GetUserById(friend.Id));

                listFriend.Add(new FriendViewModel
                {
                    UserName = friend.UserName,
                    PictureUser = Converts.ConvertByteArrToStringForImg(pictureOfUser),
                });
            }

            if (details == null)
            {
                var model = new UserDetailsViewModel()
                {
                    FirstName = "",
                    LastName = "",
                    Adress = "",
                    Age = 0,
                    ImageBrand = "",
                    Friends = listFriend,
                    Post = viewListPost
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
                    ImageBrand = Converts.ConvertByteArrToStringForImg(profilPicture),
                    Friends = listFriend,
                    Post = viewListPost
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
            var profilPicture = _postService.GetPictureProfileFromPost(userLogged);

            var ckeckFriend = _userService.CkeckForFriend(userLogged, userFriend);

            var listPost = userFriend.Posts.OrderByDescending(x => x.DateOnPost);

            var model = new FriendDetailViewModel();

            var viewListPost = new List<HomePostModel>();

            foreach (var post in listPost)
            {
                var pictureId = 0;
                int.TryParse(post.PictureId.ToString(), out pictureId);

                var commentsPost = new List<ViewModelComment>();

                foreach (var comment in post.Comments)
                {
                    var profilePicture = _postService.GetPictureProfileFromPost(_userService.GetUserByUserName(comment.Username));

                    commentsPost.Add(new ViewModelComment()
                    {
                        Username = comment.Username,
                        Description = comment.Description,
                        PictureProfile = Converts.ConvertByteArrToStringForImg(profilePicture)
                    });
                }

                var likesPost = new List<ViewModelLike>();

                foreach (var like in post.Likes)
                {
                    var pictureOfProfile = _postService.GetPictureProfileFromPost(_userService.GetUserByUserName(like.Username));

                    likesPost.Add(new ViewModelLike()
                    {
                        Username = like.Username,
                        PictureProfile = Converts.ConvertByteArrToStringForImg(pictureOfProfile)
                    });
                }

                var pictureOfUser = _postService.GetPictureProfileFromPost(_userService.GetUserById(post.UserId));

                viewListPost.Add(new HomePostModel
                {
                    PostId = post.Id,
                    UserName = _userService.GetUserById(post.UserId).UserName,
                    PictureOfUser = Converts.ConvertByteArrToStringForImg(pictureOfUser),
                    DiscriptionPost = post.Disctription,
                    DateOnPost = Converts.CreateStringDate(post.DateOnPost),
                    PicturePost = Converts.ConvertByteArrToStringForImg(_postService.GetPictureOnPost(pictureId)),
                    Likes = likesPost,
                    Comments = commentsPost
                });
            }

            var friendUser = userFriend.Friends;
            var listFriend = new List<FriendViewModel>();

            foreach (var friend in friendUser)
            {
                var pictureOfUser = _postService.GetPictureProfileFromPost(_userService.GetUserById(friend.Id));

                listFriend.Add(new FriendViewModel
                {
                    UserName = friend.UserName,
                    PictureUser = Converts.ConvertByteArrToStringForImg(pictureOfUser),
                });
            }

            if (details == null)
            {
                model.UserName = userFriend.UserName;
                model.FirstName = "undefined";
                model.LastName = "undefined";
                model.Adress = "undefined";
                model.Age = 0;
                model.ImageUser = "";
                model.CheckForFriend = ckeckFriend;
                model.Post = viewListPost;
                model.Friends = listFriend;
            }
            else
            {
                model.UserName = userFriend.UserName;
                model.FirstName = details.FirstName;
                model.LastName = details.LastName;
                model.Adress = details.Adress;
                model.Age = details.Age;
                model.ImageUser = Converts.ConvertByteArrToStringForImg(profilPicture);
                model.CheckForFriend = ckeckFriend;
                model.Post = viewListPost;
                model.Friends = listFriend;
            }


            return View("FriendDetailsView", model);
        }

        public ActionResult Pictures(string UserName)
        {
            var user = _userService.GetUserByUserName(UserName);

            var userDetail = _detailService.GetDetailByUserId(user.Id);

            var postWithPicture = _postService.GetAllPostWithPictureOnUser(user);

            var resoultPost = new List<HomePostModel>();

            foreach (var post in postWithPicture)
            {
                var pictureId = 0;
                int.TryParse(post.PictureId.ToString(), out pictureId);

                var commentsPost = new List<ViewModelComment>();

                foreach (var comment in post.Comments)
                {
                    var profilePicture = _postService.GetPictureProfileFromPost(_userService.GetUserByUserName(comment.Username));

                    commentsPost.Add(new ViewModelComment()
                    {
                        Username = comment.Username,
                        Description = comment.Description,
                        PictureProfile = Converts.ConvertByteArrToStringForImg(profilePicture)
                    });
                }

                var likesPost = new List<ViewModelLike>();

                foreach (var like in post.Likes)
                {
                    var pictureOfProfile = _postService.GetPictureProfileFromPost(_userService.GetUserByUserName(like.Username));

                    likesPost.Add(new ViewModelLike()
                    {
                        Username = like.Username,
                        PictureProfile = Converts.ConvertByteArrToStringForImg(pictureOfProfile)
                    });
                }

                var pictureOfUser = _postService.GetPictureProfileFromPost(_userService.GetUserById(post.UserId));

                resoultPost.Add(new HomePostModel
                {
                    PostId = post.Id,
                    UserName = _userService.GetUserById(post.UserId).UserName,
                    PictureOfUser = Converts.ConvertByteArrToStringForImg(pictureOfUser),
                    DiscriptionPost = post.Disctription,
                    DateOnPost = Converts.CreateStringDate(post.DateOnPost),
                    PicturePost = Converts.ConvertByteArrToStringForImg(_postService.GetPictureOnPost(pictureId)),
                    IsProfilePicture = post.IsProfilePicture,
                    Likes = likesPost,
                    Comments = commentsPost
                });
            }

            var model = new UserPicturesModel
            {
                UserName = UserName,
                PostsWithPictures = resoultPost
            };

            return View(model);
        }

        public ActionResult PicturesSave(string discriptin, HttpPostedFileBase image)
        {
            var name = User.Identity.Name;

            var user = _userService.GetUserByUserName(name);

            var userDetail = _detailService.GetDetailByUserId(user.Id);

            var picture = new byte[image.ContentLength];
            image.InputStream.Read(picture, 0, image.ContentLength);

            _postService.AddPostToUser(user, discriptin, picture, false);

            return RedirectToAction("Pictures/" + User.Identity.Name, "DetaialUser");
        }

        // POST: DetaialUser/Create
        [HttpPost]
        public ActionResult Create(UserDetailsViewModel model, HttpPostedFileBase image)
        {
            var name = User.Identity.Name;

            var user = _userService.GetUserByUserName(name);

            var userDetail = _detailService.GetDetailByUserId(user.Id);

            if (userDetail != null)
            {
                userDetail.FirstName = model.FirstName;
                userDetail.LastName = model.LastName;
                userDetail.Adress = model.Adress;
                userDetail.Age = model.Age;

                _detailService.UpdataDetail(userDetail);
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
            }

            if (image != null)
            {
                var pitureBytes = new byte[image.ContentLength];
                image.InputStream.Read(pitureBytes, 0, image.ContentLength);

                _postService.ClearProfilePictureOnPost(user);
                _postService.AddPostToUser(user, "Change your profile picture ", pitureBytes, true);

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

        public ActionResult ChangeProfilePicture(string PostId)
        {
            var name = User.Identity.Name;

            var user = _userService.GetUserByUserName(name);

            _postService.ChangeProfilePicture(user, int.Parse(PostId));

            return Json(new { status = "Success", message = name });
        }
    }
}
