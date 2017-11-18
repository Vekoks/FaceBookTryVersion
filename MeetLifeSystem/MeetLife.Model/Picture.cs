using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLife.Model
{
    public class Picture
    {
        public int Id { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public string Description { get; set; }

        public DateTime DateUploading { get; set; }

        public int UserDetailId { get; set; }

        public virtual UserDetails UserDetail { get; set; }
    }
}
