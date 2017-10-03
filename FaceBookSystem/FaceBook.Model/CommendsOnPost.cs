using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBook.Model
{
    public class CommendsOnPost
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Description { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
