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
    }
}