using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLifeClient.Models.ModelsForLiveInfo.Contracts
{
    public interface ICommentOnThePost
    {
        string Username { get; set; }

        string Description { get; set; }

        IEnumerable<ICommentOnThePost> GetDataForCommentsOnThePost(int PostId);
    }
}
