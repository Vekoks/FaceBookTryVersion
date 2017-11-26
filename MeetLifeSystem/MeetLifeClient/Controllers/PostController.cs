using MeetLife.Model;
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
    public class PostController : Controller
    {

        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly IUserDetailService _detailService;
        private readonly IAllPostInfo _infoForAllPost;
        private readonly ICommentOnThePost _infoCommentsOnThePost;
        private readonly ILikeOnPost _infoForLIkes;

        public PostController(IUserService userService,
                              IPostService postService,
                              IAllPostInfo infoPost,
                              ICommentOnThePost infoCommentsOnThePost,
                              ILikeOnPost infoLIkes,
                              IUserDetailService detailService)
        {
            this._userService = userService;
            this._postService = postService;
            this._infoForAllPost = infoPost;
            this._infoCommentsOnThePost = infoCommentsOnThePost;
            this._infoForLIkes = infoLIkes;
            this._detailService = detailService;
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetAllPostFromUsers()
        {
            var result = _infoForAllPost.GetDataAllPost().Where(x=>x.DatePost.Minute < 2 );

            var resultPost = new List<HomePostModel>();

            foreach (var post in result)
            {
                var commentsPost = new List<ViewModelComment>();
                var commentList = _infoCommentsOnThePost.GetDataForCommentsOnThePost(post.PostId).ToList();

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

                var likesPost = new List<ViewModelLike>();
                var likeList = _infoForLIkes.GetDataLikesOnThePost(post.PostId).ToList();

                foreach (var like in likeList)
                {
                    var pictureOfProfile = _detailService.GetProfilePicture(_detailService.GetDetailByUserId(_userService.GetUserByUserName(like.UserName).Id)).Image;

                    likesPost.Add(new ViewModelLike()
                    {
                        Username = like.UserName,
                        PictureProfile = Converters.ConvertByteArrToStringForImg(pictureOfProfile)
                    });
                }

                resultPost.Add(new HomePostModel
                {
                    PostId = post.PostId,
                    UserName = _userService.GetUserById(post.UserId).UserName,
                    DiscriptionPost = post.Discription,
                    PicturePost = Converters.ConvertByteArrToStringForImg(post.Picture),
                    DateOnPost = Converters.CreateStringDate(post.DatePost),
                    Likes = new List<ViewModelLike>(),
                    Comments = new List<ViewModelComment>()
                });
            }

            return Json(resultPost, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreatePost(string discriptin, HttpPostedFileBase image)
        {
            var userLogged = _userService.GetUserByUserName(this.User.Identity.Name);

            var userDetail = _detailService.GetDetailByUserId(userLogged.Id);

            var picture = new Picture();
            picture.Image = new byte[0];

            if (image != null)
            {
                picture.Image = new byte[image.ContentLength];
                image.InputStream.Read(picture.Image, 0, image.ContentLength);
                picture = _detailService.AddNewPictureOnUser(userDetail, discriptin, picture.Image);
            }

            _postService.AddPostToUser(userLogged, discriptin, picture.Image, picture.Id);

            return RedirectToAction("Index", "DetaialUser");
        }

        [HttpPost]
        public ActionResult CreateComment(string model, string postId)
        {
            var name = this.User.Identity.Name;
            var userLogged = _userService.GetUserByUserName(name);

            var PostId = int.Parse(postId);
            var CommentOfDescription = model;

            _postService.AddCommentToPost(PostId, userLogged, CommentOfDescription);

            var comments = _postService.GetPostWithId(PostId).Comments.ToList();

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateCommentOnPostWithPicture(string Description, string PictureId)
        {
            var name = this.User.Identity.Name;
            var userLogged = _userService.GetUserByUserName(name);

            var targetPost = _postService.GetPostWithPicturesWithPictureId(int.Parse(PictureId));

            _postService.AddCommentToPost(targetPost.Id, userLogged, Description);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCommentsOnThePost()
        {
            var curentPosts = _postService.GetPostWithNewComment();

            var resoult = new List<PostCommentViewModel>();

            foreach (var post in curentPosts)
            {
                resoult.Add(new PostCommentViewModel
                {
                    IdOnCurrentPost = post.Id,
                    Comments = _infoCommentsOnThePost.GetDataForCommentsOnThePost(post.Id)
                });
            }

            return Json(resoult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PutLikeOnThePOst(string model)
        {
            var name = this.User.Identity.Name;
            var userLogged = _userService.GetUserByUserName(name);

            var PostId = int.Parse(model);

            _postService.PutLikeOnThePost(PostId, userLogged);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PutLikeOnThePostWithPicture(string PictureId)
        {
            var name = this.User.Identity.Name;
            var userLogged = _userService.GetUserByUserName(name);

            var targetPost = _postService.GetPostWithPicturesWithPictureId(int.Parse(PictureId));

            _postService.PutLikeOnThePost(targetPost.Id, userLogged);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLikes()
        {
            var curentPosts = _postService.GetLikeOnThePost();

            var result = new List<PostLikeViewModel>();

            foreach (var post in curentPosts)
            {
                result.Add(new PostLikeViewModel
                {
                    IdOnCurrentPost = post.Id,
                    Likes = _infoForLIkes.GetDataLikesOnThePost(post.Id).ToList()
            });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailsPost(string id)
        {
            var targetPost = _postService.GetPostWithId(int.Parse(id));

            var commentsPost = new List<ViewModelComment>();

            var likePost = new List<ViewModelLike>();

            var likeList = _infoForLIkes.GetDataLikesOnThePost(targetPost.Id).ToList();

            var commentList = _infoCommentsOnThePost.GetDataForCommentsOnThePost(targetPost.Id).ToList();

            foreach (var like in likeList)
            {
                var profilePicture = _detailService.GetProfilePicture(_detailService.GetDetailByUserId(_userService.GetUserByUserName(like.UserName).Id)).Image;
                likePost.Add(new ViewModelLike()
                {
                    Username = like.UserName,
                    PictureProfile = Converters.ConvertByteArrToStringForImg(profilePicture)
                });
            }

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

            var model = new HomePostModel
            {
                PostId = targetPost.Id,
                UserName = _userService.GetUserById(targetPost.UserId).UserName,
                DiscriptionPost = targetPost.Disctription,
                PicturePost = Converters.ConvertByteArrToStringForImg(targetPost.ImagePost),
                DateOnPost = Converters.CreateStringDate(targetPost.DateOnPost),
                Likes = likePost,
                Comments = commentsPost
            };


            return View(model);

        }
    }
}