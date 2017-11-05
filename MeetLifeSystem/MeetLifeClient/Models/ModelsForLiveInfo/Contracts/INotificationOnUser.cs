using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLifeClient.Models.ModelsForLiveInfo
{
    public interface INotificationOnUser
    {
        string Username { get; set; }

        string Description { get; set; }

        int PostId { get; set; }

        IEnumerable<INotificationOnUser> GetDataForNotofiactionsOnUser(string IdOfLoggedUser);
    }
}
