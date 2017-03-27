using FaceBook.Data;
using FaceBook.Model;
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
        public ActionResult Index()
        {
            var db = new FaceBookDbContext();

            var users = db.Users.ToList();

            var list = new List<ChatViewModel>();

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