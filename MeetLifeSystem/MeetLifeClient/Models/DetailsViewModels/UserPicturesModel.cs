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

        public List<InfoPuctires> SrcPistures { get; set; }
    }

    public class InfoPuctires
    {
        public int PictureId { get; set; }

        public string Destriction { get; set; }

        public string SrcPistures { get; set; }

        public string Date { get; set; }

        public bool IsProfilePicture { get; set; }

        public int Likes { get; set; }

        public List<CommendsOnPost> Comments { get; set; }
    }
}