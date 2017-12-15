using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models.PostViewModels
{
    public class PostLikeViewModel
    {
        public int IdOnCurrentPost { get; set; }

        public int Likes { get; set; }
    }
}