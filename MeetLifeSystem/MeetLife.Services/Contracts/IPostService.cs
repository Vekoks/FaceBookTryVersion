using MeetLife.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLife.Services.Contracts
{
    public interface IPostService
    {
        void AddPostToUser(User User, string discriptinPost);

        void AddCommentToPost(int PostId, User User, string discriptinComment);

        List<Post> GetAllPost();

        Post GetPostWithId(int PostId);

        List<Post> GetPostWithNewComment();

        void PutLikeOnThePost(int PostId, User User);

        List<Post> GetLikeOnThePost();
    }
}
