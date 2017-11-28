using MeetLife.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models
{
    public class UserPicturesModel
    {
        public string UserName { get; set; }

        public List<ListPostWithPicture> PostsWithPictures { get; set; }
    }

    public class ListPostWithPicture
    {
        public int PostId { get; set; }

        public string Disctription { get; set; }

        public String ImagePost { get; set; }

        public bool IsProfilePicture { get; set; }

        public int DateOnPost { get; set; }

        public ICollection<CommendsOnPost> Comments { get; set; }

        public ICollection<LikesOnPost> Likes { get; set; }
    }
}