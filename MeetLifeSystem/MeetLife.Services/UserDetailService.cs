﻿using MeetLife.Data.Repository;
using MeetLife.Model;
using MeetLife.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLife.Services
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IRepository<UserDetails> _userDetailRepo;

        public  UserDetailService(IRepository<UserDetails> userDetailRepo)
        {
            this._userDetailRepo = userDetailRepo;
        }

        public void AddDetails(UserDetails userDetails)
        {
            this._userDetailRepo.Add(userDetails);
            this._userDetailRepo.SaveChanges();
        }

        public UserDetails GetDetailByUserId(string UserId)
        {
            var allDetails = this.GetAllDetails();

            return allDetails.Where(x => x.UserId == UserId).FirstOrDefault();
        }

        public IEnumerable<UserDetails> GetAllDetails()
        {
            return _userDetailRepo.All();
        }

        public void UpdataDetail(UserDetails userDetails)
        {
            this._userDetailRepo.Update(userDetails);
            this._userDetailRepo.SaveChanges();
        }

    }
}