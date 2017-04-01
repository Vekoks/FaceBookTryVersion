using FaceBook.Data;
using FaceBook.Model;
using FaceBook.Services.Contracts;
using FaceBookClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FaceBookClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            this._userService = userService;
        }

        public ActionResult Index()
        {
            var name = this.User.Identity.Name;

            var userLogged = _userService.GetUserByUserName(name);

            var list = new List<ChatViewModel>();

            if (userLogged == null)
            {
                return View(list);
            }

            //var users = userLogged.Friend.ToList();

            var users = _userService.GetAllUsers();


            foreach (var item in users)
            {

                list.Add(new ChatViewModel()
                {
                    Id = item.Id,
                    Name = item.UserName
                }
                );

            }

            return View(list);
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
    }
}