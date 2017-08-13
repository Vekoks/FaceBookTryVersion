using FaceBook.Services.Contracts;
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

        public PostService(IRepository<User> userRepo)
        {
            this._userRepo = userRepo;
        }

        public void AddPostToUser(User UserName, string discriptinPost)
        {
            UserName.Post.Add(new Post
            {
                Disctription = discriptinPost,
                DateOnPost = DateTime.Now
            });

            _userRepo.SaveChanges();
        }
    }
}
