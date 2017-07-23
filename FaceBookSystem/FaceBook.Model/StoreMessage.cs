using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBook.Model
{
    public class StoreMessage
    {
        public int Id { get; set; }

        public string Receiver { get; set; }

        public string Letter { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
