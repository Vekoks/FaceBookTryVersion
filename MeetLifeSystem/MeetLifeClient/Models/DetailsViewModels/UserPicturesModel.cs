using MeetLife.Model;
using MeetLifeClient.Models.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models.DetailsViewModels
{
    public class UserPicturesModel
    {
        public string UserName { get; set; }

        public List<HomePostModel> PostsWithPictures { get; set; }
    }

    public class ListPostWithPicture
    {
        public int PostId { get; set; }

        public string Disctription { get; set; }

        public string ImagePost { get; set; }

        public bool IsProfilePicture { get; set; }

        public int DateOnPost { get; set; }

        public ICollection<CommendsOnPost> Comments { get; set; }

        public ICollection<LikesOnPost> Likes { get; set; }
    }
}