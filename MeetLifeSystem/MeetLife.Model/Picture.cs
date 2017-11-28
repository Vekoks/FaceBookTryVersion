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
    }
}
