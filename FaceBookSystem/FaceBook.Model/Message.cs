using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBook.Model
{
    public class Message
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Letter { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
