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
                resultPost.Add(new HomePostModel
                {
                    PostId = post.PostId,
                    UserName = _userService.GetUserById(post.UserId).UserName,
                    DiscriptionPost = post.Discription,
                    DateOnPost = (int)DateTime.Now.Subtract(post.DatePost).TotalMinutes,
                    Likes = _infoForLIkes.GetDataLikesOnThePost(post.PostId).Count(),
                    Comments = _infoCommentsOnThePost.GetDataForCommentsOnThePost(post.PostId)
                });
            }

            var resultForView = resultPost.OrderBy(x => x.DateOnPost).ToList();

            return Json(resultForView, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreatePost(string discriptin)
        {
            var userLogged = _userService.GetUserByUserName(this.User.Identity.Name);

            _postService.AddPostToUser(userLogged, discriptin);

            return RedirectToAction("Index", "DetaialUser");
        }

        [HttpPost]
        public ActionResult CreateComment(string model)
        {
            var name = this.User.Identity.Name;
            var userLogged = _userService.GetUserByUserName(name);

            var arrString = model.Split(' ');

            var PostId = int.Parse(arrString[0]);
            var CommentOfDescription = arrString[1];

            _postService.AddCommentToPost(PostId, userLogged, CommentOfDescription);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCommentsOnThePost()
        {
            var curentPost = _postService.GetPostWithNewComment();

            var resoult = new PostCommentViewModel
            {
                IdOnCurrentPost = curentPost.Id,
                Comments = _infoCommentsOnThePost.GetDataForCommentsOnThePost(curentPost.Id)
            };

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
            var curentPost = _postService.GetLikeOnThePost();

            var liksOnThePost = _infoForLIkes.GetDataLikesOnThePost(curentPost.Id).Count();

            var result = new PostLikeViewModel
            {
                IdOnCurrentPost = curentPost.Id,
                Likes = liksOnThePost
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}