﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLife.Model
{
    public class InvitationForFriend
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
