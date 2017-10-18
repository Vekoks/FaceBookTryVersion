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
        private readonly ICommentOnThePost _infoCommentsOnThePost;

        public PostController(IUserService userService, IPostService postService, ICommentOnThePost infoCommentsOnThePost)
        {
            this._userService = userService;
            this._postService = postService;
            this._infoCommentsOnThePost = infoCommentsOnThePost;
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
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

            var resoultNewComments = _infoCommentsOnThePost.GetDataForCommentsOnThePost(PostId);

            return Json(resoultNewComments, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCommentsOnThePost()
        {
            var curentPost = _postService.GetPostWithNewComment();

            if (curentPost == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var resoult = new PostCommentViewModel
            {
                IdOnCurrentPost = curentPost.Id,
                Comments = _infoCommentsOnThePost.GetDataForCommentsOnThePost(curentPost.Id)
            };

            //var resoultNewComments = _infoCommentsOnThePost.GetDataForCommentsOnThePost(curentPost.Id);

            return Json(resoult, JsonRequestBehavior.AllowGet);
        }
    }
}