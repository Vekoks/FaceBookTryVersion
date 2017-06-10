using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBook.Model
{
    public class Post
    {
        public int Id { get; set; }

        public string Disctription { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

    }
}
