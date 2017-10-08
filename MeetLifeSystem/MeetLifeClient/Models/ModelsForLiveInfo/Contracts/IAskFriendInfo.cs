using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLifeClient.Models.ModelsForLiveInfo
{
    public interface IAskFriendInfo
    {
        string Name { get; set; }

        IEnumerable<IAskFriendInfo> GetDataForAskFriend(string UserIdOfLoggedUser);
    }
}
