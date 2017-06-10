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

        public PostController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePost(string discriptin)
        {
            _userService.AddPostToUser(this.User.Identity.Name, discriptin);

            return RedirectToAction("Index", "DetaialUser");
        }
    }
}