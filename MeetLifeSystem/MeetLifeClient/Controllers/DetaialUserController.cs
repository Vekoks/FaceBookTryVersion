﻿using MeetLife.Data;
using MeetLife.Data.Repository;
using MeetLife.Model;
using MeetLife.Services;
using MeetLife.Services.Contracts;
using MeetLifeClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetLifeClient.Controllers
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

            var userLogged = _userService.GetUserByUserName(name);

            var details = _detailService.GetDetailByUserId(userLogged.Id);

            if (details == null)
            {
                var model = new UserDetailsViewModel()
                {
                    FirstName = "undefined",
                    LastName = "undefined",
                    Adress = "undefined",
                    Age = 0,
                    ImageBrand = "",
                    Friends = userLogged.Friends,
                    Post = userLogged.Posts.OrderByDescending(x => x.DateOnPost)
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
                    Age = details.Age,
                    ImageBrand = this.ConvertByteArrToStringForImg(details.ImageProfil),
                    Friends = userLogged.Friends,
                    Post = userLogged.Posts.OrderByDescending(x => x.DateOnPost)
                };

                return View(model);
            }
        }

        // GET: DetaialUser/Details/UserName
        public ActionResult Details(string UserName)
        {
            var name = User.Identity.Name;

            var userLogged = _userService.GetUserByUserName(name);

            var userFriend = _userService.GetUserByUserName(UserName);

            var details = _detailService.GetDetailByUserId(userFriend.Id);

            var ckeckFriend = _userService.CkeckForFriend(userLogged, userFriend);

            var model = new FriendDetailViewModel();


            if (details == null)
            {
                model.UserName = userFriend.UserName;
                model.FirstName = "undefined";
                model.LastName = "undefined";
                model.Adress = "undefined";
                model.Age = 0;
                model.ImageUser = "";
                model.CheckForFriend = ckeckFriend;
            }
            else
            {
                model.UserName = userFriend.UserName;
                model.FirstName = details.FirstName;
                model.LastName = details.LastName;
                model.Adress = details.Adress;
                model.Age = details.Age;
                model.ImageUser = this.ConvertByteArrToStringForImg(details.ImageProfil);
                model.CheckForFriend = ckeckFriend;
            }


            return View("FriendDetailsView", model);
        }

        public ActionResult Pictures(string UserName)
        {
            var user = _userService.GetUserByUserName(UserName);

            var userDetail = _detailService.GetDetailByUserId(user.Id);

            var allPictures = _detailService.GetAllPisturesOnUser(userDetail);

            var listeInfoPuctires = new List<InfoPuctires>();

            foreach (var picture in allPictures)
            {
                listeInfoPuctires.Add(new InfoPuctires
                {
                    Destriction = picture.Description,
                    Date = (int)DateTime.Now.Subtract(picture.DateUploading).TotalMinutes,
                    SrcPistures = this.ConvertByteArrToStringForImg(picture.Image)
                });
            }

            var model = new UserPicturesModel
            {
                UserName = UserName,
                SrcPistures = listeInfoPuctires
            };

            return View(model);
        }

        public ActionResult PicturesSave(string discriptin, HttpPostedFileBase image)
        {
            var name = User.Identity.Name;

            var user = _userService.GetUserByUserName(name);

            var userDetail = _detailService.GetDetailByUserId(user.Id);

            var picutre = userDetail.ImageProfil = new byte[image.ContentLength];
            image.InputStream.Read(picutre, 0, image.ContentLength);

            _detailService.AddNewPictureOnUser(userDetail, discriptin, picutre);

            return RedirectToAction("Pictures/" + User.Identity.Name, "DetaialUser");
        }

        // POST: DetaialUser/Create
        [HttpPost]
        public ActionResult Create(UserDetailsViewModel model, HttpPostedFileBase image)
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
                userDetail.ImageProfil = new byte[image.ContentLength];
                image.InputStream.Read(userDetail.ImageProfil, 0, image.ContentLength);

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
                    UserId = user.Id,
                    ImageProfil = new byte[image.ContentLength]
                };

                image.InputStream.Read(newDetails.ImageProfil, 0, image.ContentLength);

                _detailService.AddDetails(newDetails);

            }

            return RedirectToAction("Index", "DetaialUser");
        }

        //add invitation for friend
        public ActionResult AddInvitationFriend(string UserName)
        {
            var name = User.Identity.Name;

            var userLogged = _userService.GetUserByUserName(name);

            var userFriend = _userService.GetUserByUserName(UserName);

            var newAskForFriend = new InvitationForFriend()
            {
                Username = userLogged.UserName
            };

            _userService.AddInvitationForFriend(userFriend, newAskForFriend);

            return RedirectToAction("Begining", "Home");
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


        public string ConvertByteArrToStringForImg(byte[] arr)
        {
            var base64 = Convert.ToBase64String(arr);
            var srcImg = string.Format("data:image/gif;base64,{0}", base64);

            return srcImg;
        }
    }
}
