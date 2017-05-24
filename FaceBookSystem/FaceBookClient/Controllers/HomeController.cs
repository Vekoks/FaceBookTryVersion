﻿using FaceBook.Data;
using FaceBook.Model;
using FaceBook.Services.Contracts;
using FaceBookClient.Hubs;
using FaceBookClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FaceBookClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private UsersInfo info = new UsersInfo();

        public HomeController(IUserService userService)
        {
            this._userService = userService;
        }

        public ActionResult Index()
        {
            var name = this.User.Identity.Name;
            var userLogged = _userService.GetUserByUserName(name);

            if (userLogged == null)
            {
                var notUserModel = new HomeIndexViewModel()
                {
                    AllAskForFriend = new List<InvitationForFriend>(),
                    CountAskForFriend = 0
                };

                return View(notUserModel);
            }

            var allAskForFriend = userLogged.InvitationForFriend.ToList();

            var model = new HomeIndexViewModel()
            {
                AllAskForFriend = allAskForFriend,
                CountAskForFriend = allAskForFriend.Count
            };

            return View(model);
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


        public ActionResult ConferFriend(string UserName, string confer)
        {
            var loggedUser = _userService.GetUserByUserName(User.Identity.Name);

            var userAsk = _userService.GetUserByUserName(UserName);

            if (confer.Contains("accept"))
            {
                _userService.AddNewFriend(loggedUser, userAsk);
            }

            else if (confer.Contains("delete"))
            {
                _userService.RemoveInvitationForFriend(loggedUser, userAsk);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public JsonResult ResultInfoFOrUsers()
        {
            var result = info.GetData();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}