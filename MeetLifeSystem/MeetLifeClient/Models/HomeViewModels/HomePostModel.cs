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

        public string DateOnPost { get; set; }

        public string PicturePost { get; set; }

        public bool IsProfilePicture { get; set; }

        public List<ViewModelLike> Likes { get; set; }

        public IEnumerable<ViewModelComment> Comments { get; set; }

    }

    public class ViewModelComment
    {
        public string Username { get; set; }

        public string Description { get; set; }

        public string PictureProfile { get; set; }
    }

    public class ViewModelLike
    {
        public string Username { get; set; }

        public string PictureProfile { get; set; }
    }
}