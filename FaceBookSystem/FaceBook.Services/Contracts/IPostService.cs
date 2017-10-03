using FaceBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBook.Services.Contracts
{
    public interface IPostService
    {
        void AddPostToUser(User User, string discriptinPost);

        void AddCommentToPost(int PostId, User User, string discriptinComment);
    }
}
