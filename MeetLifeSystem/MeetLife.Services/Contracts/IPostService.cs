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
        void AddPostToUser(User User, string discriptinPost, byte[] Picture, bool IsProfilePicture);

        void AddCommentToPost(int PostId, User User, string discriptinComment);

        List<Post> GetAllPost();

        Post GetPostWithId(int PostId);

        List<Post> GetPostWithNewComment();

        void PutLikeOnThePost(int PostId, User User);

        List<Post> GetLikeOnThePost();

        byte[] GetPictureProfileFromPost(User LoggedUser);

        List<Post> GetAllPostWithPictureOnUser(User User);

        void ChangeProfilePicture(User LoggedUser, int NewPictureId);

        byte[] GetPictureOnPost(int? PostId);

        void ClearProfilePictureOnPost(User User);

        void MakeSeenNotification(User User, int NotificationId);
    }
}
