using MeetLife.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetLife.Model;
using MeetLife.Data.Repository;

namespace MeetLife.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Post> _postRepo;
        private readonly IRepository<Picture> _pictureRepo;

        public PostService(IRepository<User> userRepo, IRepository<Post> postRepo, IRepository<Picture> pictureRepo)
        {
            this._userRepo = userRepo;
            this._postRepo = postRepo;
            this._pictureRepo = pictureRepo;
        }

        public void AddPostToUser(User User, string discriptinPost, byte[] Picture, bool IsProfilePicture)
        {
            int? pictureId = null;
            if (Picture.Length != 0)
            {
                var picturePost = new Picture()
                {
                    Image = Picture
                };

                _pictureRepo.Add(picturePost);
                _pictureRepo.SaveChanges();

                pictureId = picturePost.Id;
            }

            User.Posts.Add(new Post
            {
                Disctription = discriptinPost,
                DateOnPost = DateTime.Now,
                IsProfilePicture = IsProfilePicture,
                PictureId = pictureId
            });

            _userRepo.SaveChanges();
        }

        public void AddCommentToPost(int PostId, User UserWriteComment, string discriptinComment)
        {
            var targetPost = _postRepo.All().Where(x => x.Id == PostId).FirstOrDefault();

            targetPost.Comments.Add(new CommendsOnPost()
            {
                Username = UserWriteComment.UserName,
                Description = discriptinComment
            });

            targetPost.WorkOnComment = true;

            //create notification
            var userWhoWritePost = targetPost.User;

            userWhoWritePost.Notifications.Add(new Notification
            {
                UserName = UserWriteComment.UserName,
                Disctription = UserWriteComment.UserName + " write comment on your post",
                Post = targetPost,
                IsSaw = false
            });

            _postRepo.SaveChanges();
        }

        public List<Post> GetAllPost()
        {
            return _postRepo.All().ToList();
        }

        public Post GetPostWithId(int PostId)
        {
            return this.GetAllPost().Where(x => x.Id == PostId).FirstOrDefault();
        }

        public List<Post> GetPostWithNewComment()
        {
            var currentPosts = GetAllPost().Where(x => x.WorkOnComment == true).ToList();

            foreach (var post in currentPosts)
            {
                post.WorkOnComment = false;
            }

            _postRepo.SaveChanges();

            return currentPosts;
        }

        public void PutLikeOnThePost(int PostId, User UserPutLike)
        {
            var targetPost = _postRepo.All().Where(x => x.Id == PostId).FirstOrDefault();

            targetPost.Likes.Add(new LikesOnPost()
            {
                Username = UserPutLike.UserName
            });

            targetPost.WorkOnLike = true;

            //create notification
            var userWhoWritePost = targetPost.User;

            userWhoWritePost.Notifications.Add(new Notification
            {
                UserName = UserPutLike.UserName,
                Disctription = UserPutLike.UserName + " like your post",
                Post = targetPost,
                IsSaw = false
            });

            _postRepo.SaveChanges();
        }

        public List<Post> GetLikeOnThePost()
        {
            var currentPosts = GetAllPost().Where(x => x.WorkOnLike == true).ToList();

            foreach (var post in currentPosts)
            {
                post.WorkOnLike = false;
            }

            _postRepo.SaveChanges();

            return currentPosts;
        }

        public byte[] GetPictureProfileFromPost(User LoggedUser)
        {
            if (LoggedUser.Posts.Count == 0)
            {
                return new byte[0];
            }

            var profilePicture = new byte[0];

            try
            {
                var post = LoggedUser.Posts.Where(x => x.IsProfilePicture == true).FirstOrDefault();

                profilePicture = this.GetPictureOnPost(post.PictureId);
            }
            catch (Exception)
            {

            }

            return profilePicture;

        }

        public List<Post> GetAllPostWithPictureOnUser(User user)
        {
            return user.Posts.Where(x=>x.PictureId != null).ToList();
        }

        public void ChangeProfilePicture(User LoggedUser, int NewPicturePostId)
        {
            var oldProfilePicture = LoggedUser.Posts.Where(x => x.IsProfilePicture == true).FirstOrDefault();
            oldProfilePicture.IsProfilePicture = false;

            var newProfilePicture = LoggedUser.Posts.Where(x => x.Id == NewPicturePostId).FirstOrDefault();
            newProfilePicture.IsProfilePicture = true;

            _postRepo.SaveChanges();
        }

        public byte[] GetPictureOnPost(int? PostId)
        {
            try
            {
                return _pictureRepo.All().Where(x => x.Id == PostId).FirstOrDefault().Image;
            }
            catch (Exception)
            {

            }

            return new byte[0];
        }

        public void ClearProfilePictureOnPost(User User)
        {
            if (User.Posts.Count != 0)
            {
                var oldPostWithProfilePicture = User.Posts.Where(x => x.IsProfilePicture == true).FirstOrDefault();

                if (oldPostWithProfilePicture != null)
                {
                    oldPostWithProfilePicture.IsProfilePicture = false;

                    _postRepo.SaveChanges();
                }

            }
            
        }

        public void MakeSeenNotification(User User, int NotificationId)
        {
            var targetNotification = User.Notifications.Where(x => x.Id == NotificationId).FirstOrDefault();

            if (!targetNotification.IsSaw)
            {
                targetNotification.IsSaw = true;
            }
           

            _postRepo.SaveChanges();
        }
    }
}
