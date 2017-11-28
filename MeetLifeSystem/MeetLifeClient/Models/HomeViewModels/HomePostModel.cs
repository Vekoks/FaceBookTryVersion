using MeetLifeClient.Models.ModelsForLiveInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models
{
    public class HomePostModel
    {
        public int PostId { get; set; }

        public string UserName { get; set; }

        public string DiscriptionPost { get; set; }

        public int DateOnPost { get; set; }

        public string PicturePost { get; set; }

        public int Likes { get; set; }

        public IEnumerable<ICommentOnThePost> Comments { get; set; }

    }
}