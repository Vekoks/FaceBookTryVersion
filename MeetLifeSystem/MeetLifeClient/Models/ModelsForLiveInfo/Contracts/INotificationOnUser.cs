using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLifeClient.Models.ModelsForLiveInfo.Contracts
{
    public interface INotificationOnUser
    {
        int NotificationId { get; set; }

        string Username { get; set; }

        string Description { get; set; }

        int PostId { get; set; }

        bool IsSaw { get; set; }

        IEnumerable<INotificationOnUser> GetDataForNotofiactionsOnUser(string IdOfLoggedUser);
    }
}
