﻿using FaceBook.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceBook.Model;
using FaceBook.Data.Repository;

namespace FaceBook.Services
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

            _postRepo.SaveChanges();
        }
    }
}
