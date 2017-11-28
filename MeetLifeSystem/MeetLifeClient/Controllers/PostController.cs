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
        private readonly IAllPostInfo _infoForAllPost;
        private readonly ICommentOnThePost _infoCommentsOnThePost;
        private readonly ILikeOnPost _infoForLIkes;

        public PostController(IUserService userService,
                              IPostService postService,
                              IAllPostInfo infoPost,
                              ICommentOnThePost infoCommentsOnThePost,
                              ILikeOnPost infoLIkes)
        {
            this._userService = userService;
            this._postService = postService;
            this._infoForAllPost = infoPost;
            this._infoCommentsOnThePost = infoCommentsOnThePost;
            this._infoForLIkes = infoLIkes;
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetAllPostFromUsers()
        {
            var result = _infoForAllPost.GetDataAllPost();

            var resultPost = new List<HomePostModel>();

            foreach (var post in result)
            {
                var pictureId = 0;

                int.TryParse(post.PictureId.ToString(), out pictureId);

                resultPost.Add(new HomePostModel
                {
                    PostId = post.PostId,
                    UserName = _userService.GetUserById(post.UserId).UserName,
                    DiscriptionPost = post.Discription,
                    DateOnPost = (int)DateTime.Now.Subtract(post.DatePost).TotalMinutes,
                    PicturePost = Converts.ConvertByteArrToStringForImg(_postService.GetPictureOnPost(pictureId)),
                    Likes = _infoForLIkes.GetDataLikesOnThePost(post.PostId).Count(),
                    Comments = _infoCommentsOnThePost.GetDataForCommentsOnThePost(post.PostId)
                });
            }

            var resultForView = resultPost.OrderBy(x => x.DateOnPost).ToList();

            return Json(resultForView, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreatePost(string discriptin, HttpPostedFileBase image)
        {
            var userLogged = _userService.GetUserByUserName(this.User.Identity.Name);

            var pitureBytes = new byte[0];

            if (image != null)
            {
                pitureBytes = new byte[image.ContentLength];
                image.InputStream.Read(pitureBytes, 0, image.ContentLength);
            }

            _postService.AddPostToUser(userLogged, discriptin, pitureBytes, false);

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

        [HttpGet]
        public JsonResult GetLikes()
        {
            var curentPosts = _postService.GetLikeOnThePost();

            var result = new List<PostLikeViewModel>();

            foreach (var post in curentPosts)
            {
                var liksOnThePost = _infoForLIkes.GetDataLikesOnThePost(post.Id).Count();

                result.Add(new PostLikeViewModel
                {
                    IdOnCurrentPost = post.Id,
                    Likes = liksOnThePost
                });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailsPost(string id)
        {
            var targetPost = _postService.GetPostWithId(int.Parse(id));

            var model = new HomePostModel
            {
                PostId = targetPost.Id,
                UserName = _userService.GetUserById(targetPost.UserId).UserName,
                DiscriptionPost = targetPost.Disctription,
                DateOnPost = (int)DateTime.Now.Subtract(targetPost.DateOnPost).TotalMinutes,
                Likes = _infoForLIkes.GetDataLikesOnThePost(targetPost.Id).Count(),
                Comments = _infoCommentsOnThePost.GetDataForCommentsOnThePost(targetPost.Id)
            };


            return View(model);

        }
    }
}