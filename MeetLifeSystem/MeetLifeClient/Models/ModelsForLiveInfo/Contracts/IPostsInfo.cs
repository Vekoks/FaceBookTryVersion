using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLifeClient.Models.ModelsForLiveInfo
{
    public interface IPostsInfo
    {
        int PostID { get; set; }

        string Description { get; set; }

        byte[] ImagePost { get; set; }

        DateTime DateOnPost { get; set; }

        string UserId { get; set; }

        IEnumerable<IPostsInfo> GetPosts();
    }
}
