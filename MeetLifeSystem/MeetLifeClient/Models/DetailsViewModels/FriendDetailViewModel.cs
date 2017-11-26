using MeetLife.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models
{
    public class FriendDetailViewModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Adress { get; set; }

        public string ImageUser { get; set; }

        public bool CheckForFriend { get; set; }

        public IEnumerable<User> Friends { get; set; }

        public IEnumerable<HomePostModel> Post { get; set; }
    }
}