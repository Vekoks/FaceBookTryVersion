using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLifeClient.Models.ModelsForLiveInfo
{
    public interface IAllPostInfo
    {
        int PostId { get; set; }

        string Discription { get; set; }

        DateTime DatePost { get; set; }

        byte[] Picture { get; set; }

        string UserId { get; set; }

        IEnumerable<IAllPostInfo> GetDataAllPost();
    }
}
