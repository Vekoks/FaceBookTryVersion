using MeetLifeClient.Models.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models.PostViewModels
{
    public class PostLikeViewModel
    {
        public int IdOnCurrentPost { get; set; }

        public int LikesCount { get; set; }

        public IEnumerable<ViewModelLike> Likes { get; set; }
    }
}