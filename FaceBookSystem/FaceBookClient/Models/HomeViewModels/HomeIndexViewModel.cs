﻿using FaceBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaceBookClient.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<InvitationForFriend> AllAskForFriend { get; set; }

        public int CountAskForFriend { get; set; }

        public List<Message> Messages { get; set; }
    }
}