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
            User.Post.Add(new Post
            {
                Disctription = discriptinPost,
                DateOnPost = DateTime.Now
            });

            _userRepo.SaveChanges();
        }

        public void AddCommentToPost(int PostId, User User, string discriptinComment)
        {
            var TargetPost = _postRepo.All().Where(x => x.Id == PostId).FirstOrDefault();

            TargetPost.Comments.Add(new CommendsOnPost()
            {
                Username = User.UserName,
                Description = discriptinComment
            });

            TargetPost.WorkOnComment = true;

            _postRepo.SaveChanges();
        }

        public List<Post> GetAllPost()
        {
            return _postRepo.All().ToList();
        }

        public Post GetPostWithNewComment()
        {
            var currentPost = GetAllPost().Where(x => x.WorkOnComment == true).FirstOrDefault();

            if (currentPost == null)
            {
                return new Post()
                {
                    Id = 100
                };
            }

            currentPost.WorkOnComment = false;

            _postRepo.SaveChanges();

            return currentPost;
        }

        public void PutLikeOnThePost(int PostId, User User)
        {
            var TargetPost = _postRepo.All().Where(x => x.Id == PostId).FirstOrDefault();

            TargetPost.Likes.Add(new LikesOnPost()
            {
                Username = User.UserName
            });

            TargetPost.WorkOnLike = true;

            _postRepo.SaveChanges();
        }

        public Post GetLikeOnThePost()
        {
            var currentPost = GetAllPost().Where(x => x.WorkOnLike == true).FirstOrDefault();

            if (currentPost == null)
            {
                return new Post()
                {
                    Id = 100
                };
            }

            currentPost.WorkOnLike = false;

            _postRepo.SaveChanges();

            return currentPost;
        }
    }
}
