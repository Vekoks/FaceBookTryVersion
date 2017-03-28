using FaceBook.Data;
using FaceBook.Data.Repository;
using FaceBook.Model;
using FaceBook.Services;
using FaceBook.Services.Contracts;
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
        private readonly IUserDetailService _detailService;
        private readonly IUserService _userService;

        public DetaialUserController(IUserService userService, IUserDetailService detailService)
        {
            this._detailService = detailService;
            this._userService = userService;
        }

        // GET: DetaialUser
        public ActionResult Index()
        {
          
            var name = User.Identity.Name;

            var user = _userService.GetUserByUserName(name);

            var details = _detailService.GetDetailByUserId(user.Id);

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
            var name = User.Identity.Name;

            var user = _userService.GetUserByUserName(name);

            var userDetail = _detailService.GetDetailByUserId(user.Id);

            if (userDetail != null)
            {
                userDetail.FirstName = model.FirstName;
                userDetail.LastName = model.LastName;
                userDetail.Adress = model.Adress;
                userDetail.Age = model.Age;

                _detailService.UpdataDetail(userDetail);
            }

            else
            {

                var newDetails = new UserDetails()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Adress = model.Adress,
                    Age = model.Age,
                    UserId = user.Id
                };

                _detailService.AddDetails(newDetails);

            }

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
