using MeetLife.Data;
using MeetLife.Data.Repository;
using MeetLife.Model;
using MeetLife.Services;
using MeetLife.Services.Contracts;
using MeetLifeClient.Models;
using MeetLifeClient.Models.ModelsForLiveInfo;
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
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly ICommentOnThePost _infoCommentsOnThePost;
        private readonly ILikeOnPost _infoForLIkes;

        public DetaialUserController(IUserService userService, 
                                     IUserDetailService detailService, 
                                     IPostService postService,
                                     ICommentOnThePost infoCommentsOnThePost,
                                     ILikeOnPost infoLIkes)
        {
            this._detailService = detailService;
            this._userService = userService;
            this._postService = postService;
            this._infoCommentsOnThePost = infoCommentsOnThePost;
            this._infoForLIkes = infoLIkes;
        }

        // GET: DetaialUser
        public ActionResult Index()
        {
            var name = User.Identity.Name;

            var userLogged = _userService.GetUserByUserName(name);

            var details = _detailService.GetDetailByUserId(userLogged.Id);

            var profilPicture = _detailService.GetProfilePicture(details);

            var resultPost = new List<HomePostModel>();

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
                    Post = resultPost
                };

                return View(model);
            }
            else
            {
                var postList = userLogged.Posts.OrderByDescending(x => x.DateOnPost);

                foreach (var post in postList)
                {
                    var commentsPost = new List<ViewModelComment>();

                    var commentList = _infoCommentsOnThePost.GetDataForCommentsOnThePost(post.Id).ToList();

                    foreach (var comment in commentList)
                    {
                        var profilePicture = _detailService.GetProfilePicture(_detailService.GetDetailByUserId(_userService.GetUserByUserName(comment.Username).Id)).Image;

                        commentsPost.Add(new ViewModelComment()
                        {
                            Username = comment.Username,
                            Description = comment.Description,
                            PictureProfile = Converters.ConvertByteArrToStringForImg(profilePicture)
                        });
                    }

                    var likePost = new List<ViewModelLike>();
                    var likeList = _infoForLIkes.GetDataLikesOnThePost(post.Id).ToList();

                    foreach (var like in likeList)
                    {
                        var profilePicture = _detailService.GetProfilePicture(_detailService.GetDetailByUserId(_userService.GetUserByUserName(like.UserName).Id)).Image;
                        likePost.Add(new ViewModelLike()
                        {
                            Username = like.UserName,
                            PictureProfile = Converters.ConvertByteArrToStringForImg(profilePicture)
                        });
                    }

                    resultPost.Add(new HomePostModel
                    {
                        PostId = post.Id,
                        UserName = _userService.GetUserById(post.UserId).UserName,
                        DiscriptionPost = post.Disctription,
                        PicturePost = Converters.ConvertByteArrToStringForImg(post.ImagePost),
                        DateOnPost = Converters.CreateStringDate(post.DateOnPost),
                        Likes = likePost,
                        Comments = commentsPost
                    });
                }

                var model = new UserDetailsViewModel()
                {
                    FirstName = details.FirstName,
                    LastName = details.LastName,
                    Adress = details.Adress,
                    Age = details.Age,
                    ImageBrand = Converters.ConvertByteArrToStringForImg(profilPicture.Image),
                    Friends = userLogged.Friends,
                    Post = resultPost
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
            var resultPost = new List<HomePostModel>();

            if (details == null)
            {
                model.UserName = userFriend.UserName;
                model.FirstName = "undefined";
                model.LastName = "undefined";
                model.Adress = "undefined";
                model.Age = 0;
                model.ImageUser = "";
                model.CheckForFriend = ckeckFriend;
                model.Post = resultPost;
                model.Friends = new List<User>();
            }
            else
            {
                var postList = userFriend.Posts.OrderByDescending(x => x.DateOnPost);

                foreach (var post in postList)
                {
                    var commentsPost = new List<ViewModelComment>();

                    var commentList = _infoCommentsOnThePost.GetDataForCommentsOnThePost(post.Id).ToList();

                    foreach (var comment in commentList)
                    {
                        var profilePicture = _detailService.GetProfilePicture(_detailService.GetDetailByUserId(_userService.GetUserByUserName(comment.Username).Id)).Image;

                        commentsPost.Add(new ViewModelComment()
                        {
                            Username = comment.Username,
                            Description = comment.Description,
                            PictureProfile = Converters.ConvertByteArrToStringForImg(profilePicture)
                        });
                    }

                    var likePost = new List<ViewModelLike>();
                    var likeList = _infoForLIkes.GetDataLikesOnThePost(post.Id).ToList();

                    foreach (var like in likeList)
                    {
                        var profilePicture = _detailService.GetProfilePicture(_detailService.GetDetailByUserId(_userService.GetUserByUserName(like.UserName).Id)).Image;
                        likePost.Add(new ViewModelLike()
                        {
                            Username = like.UserName,
                            PictureProfile = Converters.ConvertByteArrToStringForImg(profilePicture)
                        });
                    }

                    resultPost.Add(new HomePostModel
                    {
                        PostId = post.Id,
                        UserName = _userService.GetUserById(post.UserId).UserName,
                        DiscriptionPost = post.Disctription,
                        PicturePost = Converters.ConvertByteArrToStringForImg(post.ImagePost),
                        DateOnPost = Converters.CreateStringDate(post.DateOnPost),
                        Likes = likePost,
                        Comments = commentsPost
                    });
                }

                model.UserName = userFriend.UserName;
                model.FirstName = details.FirstName;
                model.LastName = details.LastName;
                model.Adress = details.Adress;
                model.Age = details.Age;
                model.ImageUser = Converters.ConvertByteArrToStringForImg(profilPicture.Image);
                model.CheckForFriend = ckeckFriend;
                model.Post = resultPost;
                model.Friends = userFriend.Friends;
            }


            return View("FriendDetailsView", model);
        }


        [HttpGet]
        public JsonResult SearchUsers()
        {
            var searchUsers = _userService.GetAllUsers().ToList();

            var resoult = new List<DeatailSearchUserModel>();

            foreach (var user in searchUsers)
            {
                var profilePicture = _detailService.GetDetailByUserId(user.Id).Pitures.Where(x => x.IsProfilPicture == true).FirstOrDefault();

                resoult.Add(new DeatailSearchUserModel()
                {
                    UserName = user.UserName,
                    ProfilePicture = Converters.ConvertByteArrToStringForImg(profilePicture.Image)
                });
            }

            return Json(resoult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Pictures(string UserName)
        {
            var user = _userService.GetUserByUserName(UserName);

            var userDetail = _detailService.GetDetailByUserId(user.Id);

            var allPictures = _detailService.GetAllPisturesOnUser(userDetail);

            var listeInfoPuctires = new List<InfoPuctires>();

            foreach (var picture in allPictures)
            {
                var postWithTargetPictures = _postService.GetPostWithPicturesWithPictureId(picture.Id);

                if (postWithTargetPictures == null)
                {
                    postWithTargetPictures = new Post
                    {
                        Likes = new List<LikesOnPost>(),
                        Comments = new List<CommendsOnPost>()
                    };
                }

                listeInfoPuctires.Add(new InfoPuctires
                {
                    PictureId = picture.Id,
                    Destriction = picture.Description,
                    Date = Converters.CreateStringDate(picture.DateUploading),
                    SrcPistures = Converters.ConvertByteArrToStringForImg(picture.Image),
                    IsProfilePicture = picture.IsProfilPicture,
                    Likes = postWithTargetPictures.Likes.Count(),
                    Comments = postWithTargetPictures.Comments.ToList()
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

            var newPictures = _detailService.AddNewPictureOnUser(userDetail, discriptin, picutre);

            _postService.AddPostToUser(user, discriptin, picutre, newPictures.Id);

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
    }
}
