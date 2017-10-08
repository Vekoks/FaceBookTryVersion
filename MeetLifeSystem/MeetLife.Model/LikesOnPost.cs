using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLife.Model
{
    public class LikesOnPost
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
