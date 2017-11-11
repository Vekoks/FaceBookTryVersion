using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeetLife.Model
{
    public class User : IdentityUser
    {
        private ICollection<Notification> notifications;

        private ICollection<Post> posts;

        private ICollection<MissMessage> messagess;

        private ICollection<User> friends;

        private ICollection<InvitationForFriend> askForFriends;

        public User()
        {
            this.notifications = new List<Notification>();
            this.posts = new List<Post>();
            this.messagess = new List<MissMessage>();
            this.friends = new List<User>();
            this.askForFriends = new LinkedList<InvitationForFriend>();
        }

        public override string Id { get; set; }

        public override string UserName { get; set; }

        public override string Email { get; set; }

        public bool IsOnline { get; set; }

        public virtual ICollection<Notification> Notifications
        {
            get { return notifications; }
            set { notifications = value; }
        }

        public virtual ICollection<Post> Posts
        {
            get { return posts; }
            set { posts = value; }
        }
        
        public virtual ICollection<MissMessage> MissMessages
        {
            get { return messagess; }
            set { messagess = value; }
        }

        public virtual ICollection<User> Friends
        {
            get { return friends; }
            set { friends = value; }
        }

        public virtual ICollection<InvitationForFriend> InvitationForFriends
        {
            get { return askForFriends; }
            set { askForFriends = value; }
        }


        public ClaimsIdentity GenerateUserIdentity(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }
    }
}
