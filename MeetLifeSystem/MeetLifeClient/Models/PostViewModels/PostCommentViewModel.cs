using MeetLifeClient.Models.ModelsForLiveInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models
{
    public class PostCommentViewModel
    {
        public int IdOnCurrentPost { get; set; }

        public IEnumerable<ICommentOnThePost> Comments { get; set; }
    }
}