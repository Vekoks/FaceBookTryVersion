using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBook.Model
{
    public class MissMessage
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
