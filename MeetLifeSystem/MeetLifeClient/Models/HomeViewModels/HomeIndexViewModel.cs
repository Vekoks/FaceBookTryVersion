using MeetLife.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<InvitationForFriend> AllAskForFriend { get; set; }

        public int CountAskForFriend { get; set; }

        public List<MissMessage> Messages { get; set; }

        public List<HomePostModel> Posts { get; set; }
    }
}