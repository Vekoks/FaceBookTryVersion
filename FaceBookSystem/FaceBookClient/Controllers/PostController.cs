using FaceBook.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FaceBookClient.Controllers
{
    public class PostController : Controller
    {

        private readonly IUserService _userService;
        private readonly IPostService _postService;

        public PostController(IUserService userService, IPostService postService)
        {
            this._userService = userService;
            this._postService = postService;
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePost(string discriptin)
        { 
            var userLogged = _userService.GetUserByUserName(this.User.Identity.Name);

            _postService.AddPostToUser(userLogged, discriptin);

            return RedirectToAction("Index", "DetaialUser");
        }

        public ActionResult CreateComment(string model)
        {
            var name = this.User.Identity.Name;
            var userLogged = _userService.GetUserByUserName(name);

            var arrString = model.Split(' ');

            var PostId = int.Parse(arrString[0]);
            var CommentOfDescription = arrString[1];

            _postService.AddCommentToPost(PostId, userLogged, CommentOfDescription);

            return Json(new { status = "Success", message = "Success" });
        }
    }
}