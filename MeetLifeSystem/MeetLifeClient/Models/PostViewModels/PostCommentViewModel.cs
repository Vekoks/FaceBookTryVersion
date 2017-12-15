using MeetLifeClient.Models.ModelsForLiveInfo;
using MeetLifeClient.Models.ModelsForLiveInfo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models.PostViewModels
{
    public class PostCommentViewModel
    {
        public int IdOnCurrentPost { get; set; }

        public IEnumerable<ICommentOnThePost> Comments { get; set; }
    }
}