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
    public class DetaialUserController : Controller
    {
        // GET: DetaialUser
        public ActionResult Index()
        {
            var db = new FaceBookDbContext();

            var name = User.Identity.Name;

            var idOfUser = db.Users.Where(x => x.UserName == name).FirstOrDefault();

            var details = db.UserDetails.Where(x => x.UserId == idOfUser.Id).FirstOrDefault();

            if (details == null)
            {
                var model = new UserDetailsViewModel()
                {
                    FirstName = "undefined",
                    LastName = "undefined",
                    Adress = "undefined",
                    Age = 0
                };

                return View(model);
            }
            else
            {

                var model = new UserDetailsViewModel()
                {
                    FirstName = details.FirstName,
                    LastName = details.LastName,
                    Adress = details.Adress,
                    Age = details.Age
                };

                return View(model);
            }


            
        }

        // GET: DetaialUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DetaialUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DetaialUser/Create
        [HttpPost]
        public ActionResult Create(UserDetailsViewModel model)
        {
            var db = new FaceBookDbContext();

            var name = User.Identity.Name;

            var idOfUser = db.Users.Where(x => x.UserName == name).FirstOrDefault();

            var userDetail = db.UserDetails.Where(x => x.UserId == idOfUser.Id).FirstOrDefault();

            if (userDetail != null)
            {
                userDetail.FirstName = model.FirstName;
                userDetail.LastName = model.LastName;
                userDetail.Adress = model.Adress;
                userDetail.Age = model.Age;
            }

            else
            {

                db.UserDetails.Add(new UserDetails()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Adress = model.Adress,
                    Age = model.Age,
                    UserId = idOfUser.Id
                });

            }

            db.SaveChanges();

            return RedirectToAction("Index", "DetaialUser");
        }

        // GET: DetaialUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetaialUser/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DetaialUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetaialUser/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
