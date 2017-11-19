using MeetLifeClient.Models.ModelsForLiveInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models
{
    public class PostLikeViewModel
    {
        public int IdOnCurrentPost { get; set; }

        public List<LikeOnPost> Likes { get; set; }
    }
}