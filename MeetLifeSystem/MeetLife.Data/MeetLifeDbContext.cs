using MeetLife.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLife.Data
{
    public class MeetLifeDbContext : IdentityDbContext<User>, IMeetLifeDbContext
    {
        public MeetLifeDbContext() : base("MeetLifeSystem")
        {

        }

        public override IDbSet<User> Users { get; set; }

        public IDbSet<UserDetails> UserDetails { get; set; }

        public IDbSet<InvitationForFriend> InvitationsForFriend { get; set; }

        public IDbSet<MissMessage> Messages { get; set; }

        public IDbSet<Notification> Notifications { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<CommendsOnPost> Comments { get; set; }

        public IDbSet<StoreMessage> StoreMessages { get; set; }


        public static MeetLifeDbContext Create()
        {
            return new MeetLifeDbContext();
        }
    }
}
