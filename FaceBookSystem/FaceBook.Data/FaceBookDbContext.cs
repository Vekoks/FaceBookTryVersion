using FaceBook.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBook.Data
{
    public class FaceBookDbContext : IdentityDbContext<User>, IFaceBookDbContext
    {
        public FaceBookDbContext() : base("FaceBookSystem")
        {

        }

        public override IDbSet<User> Users { get; set; }

        public IDbSet<UserDetails> UserDetails { get; set; }

        public static FaceBookDbContext Create()
        {
            return new FaceBookDbContext();
        }
    }
}
