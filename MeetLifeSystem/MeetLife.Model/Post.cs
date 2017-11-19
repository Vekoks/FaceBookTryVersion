using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLife.Model
{
    public class Post
    {
        private ICollection<CommendsOnPost> comments;
        private ICollection<LikesOnPost> likes;

        public Post ()
        {
            this.comments = new List<CommendsOnPost>();
            this.likes = new List<LikesOnPost>();
        }

        public int Id { get; set; }

        public string Disctription { get; set; }

        [Column(TypeName = "image")]
        public byte[] ImagePost{ get; set; }

        public int PictureId { get; set; }

        public DateTime DateOnPost { get; set; }

        public bool WorkOnComment { get; set; }

        public bool WorkOnLike { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<CommendsOnPost> Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public virtual ICollection<LikesOnPost> Likes
        {
            get { return likes; }
            set { likes = value; }
        }

    }
}
