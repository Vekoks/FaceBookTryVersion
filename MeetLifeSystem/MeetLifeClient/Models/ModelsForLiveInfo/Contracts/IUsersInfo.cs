using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLifeClient.Models.ModelsForLiveInfo
{
    public interface IFrieandsInfo
    {
        string Name { get; set; }

        bool IsOnline { get; set; }

        IEnumerable<IFrieandsInfo> GetFriends(string LoggedUserId);
    }
}
