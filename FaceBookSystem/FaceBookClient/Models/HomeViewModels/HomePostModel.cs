﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaceBookClient.Models
{
    public class HomePostModel
    {
        public int PostId { get; set; }

        public string UserName { get; set; }

        public string DiscriptionPost { get; set; }

        public int DateOnPost { get; set; }
    }
}