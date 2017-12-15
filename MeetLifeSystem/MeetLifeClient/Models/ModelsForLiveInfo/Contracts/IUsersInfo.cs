using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLifeClient.Models.ModelsForLiveInfo.Contracts
{
    public interface IUsersInfo
    {
        string Name { get; set; }

        bool IsOnline { get; set; }

        IEnumerable<IUsersInfo> GetData(string LoggedUserId);
    }
}
