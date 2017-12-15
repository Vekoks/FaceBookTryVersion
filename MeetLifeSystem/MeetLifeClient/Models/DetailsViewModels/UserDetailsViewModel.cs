using MeetLife.Model;
using MeetLifeClient.Models.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models.DetailsViewModels
{
    public class UserDetailsViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Adress { get; set; }

        public string ImageBrand { get; set; }

        public IEnumerable<User> Friends { get; set; }

        public IEnumerable<HomePostModel> Post { get; set; }

    }
}