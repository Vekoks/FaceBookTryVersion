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

        public PostService(IRepository<User> userRepo, IRepository<Post> postRepo)
        {
            this._userRepo = userRepo;
            this._postRepo = postRepo;
        }

        public void AddPostToUser(User User, string discriptinPost)
        {
            User.Posts.Add(new Post
            {
                Disctription = discriptinPost,
                DateOnPost = DateTime.Now
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
                Post = targetPost
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
                Post = targetPost
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
    }
}
