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
        void AddPostToUser(User UserName, string discriptinPost);
    }
}
