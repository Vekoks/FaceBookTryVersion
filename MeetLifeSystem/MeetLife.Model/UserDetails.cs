using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLife.Model
{
    public class UserDetails
    {
        private ICollection<Picture> pitures;

        public UserDetails()
        {
            this.pitures = new List<Picture>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Adress { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Picture> Pitures
        {
            get { return pitures; }
            set { pitures = value; }
        }
    }
}
