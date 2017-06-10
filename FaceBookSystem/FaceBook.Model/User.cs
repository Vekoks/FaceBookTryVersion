using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FaceBook.Model
{
    public class User : IdentityUser
    {
        private ICollection<Notification> notification;

        private ICollection<Post> post;

        private ICollection<Message> messages;

        private ICollection<User> friend;

        private ICollection<InvitationForFriend> askForFriend;

        public User()
        {
            this.notification = new List<Notification>();
            this.post = new List<Post>();
            this.messages = new List<Message>();
            this.friend = new List<User>();
            this.askForFriend = new LinkedList<InvitationForFriend>();
        }

        public override string Id { get; set; }

        public override string UserName { get; set; }

        public override string Email { get; set; }

        public bool IsOnline { get; set; }

        public virtual ICollection<Notification> Notification
        {
            get { return notification; }
            set { notification = value; }
        }

        public virtual ICollection<Post> Post
        {
            get { return post; }
            set { post = value; }
        }
        
        public virtual ICollection<Message> Message
        {
            get { return messages; }
            set { messages = value; }
        }

        public virtual ICollection<User> Friend
        {
            get { return friend; }
            set { friend = value; }
        }

        public virtual ICollection<InvitationForFriend> InvitationForFriend
        {
            get { return askForFriend; }
            set { askForFriend = value; }
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
