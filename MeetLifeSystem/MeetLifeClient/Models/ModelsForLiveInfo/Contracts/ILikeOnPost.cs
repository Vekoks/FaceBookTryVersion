﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLifeClient.Models.ModelsForLiveInfo.Contracts
{
    public interface ILikeOnPost
    {
        int PostId { get; set; }

        string UserName { get; set; }

        IEnumerable<LikeOnPost> GetDataLikesOnThePost(int PostId);
    }
}
