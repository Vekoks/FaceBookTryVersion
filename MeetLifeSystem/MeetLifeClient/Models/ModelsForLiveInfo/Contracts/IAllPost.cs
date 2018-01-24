using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLifeClient.Models.ModelsForLiveInfo.Contracts
{
    public interface IAllPost
    {
        int PostId { get; set; }

        IEnumerable<IAllPost> GetDataAllPost();
    }
}
