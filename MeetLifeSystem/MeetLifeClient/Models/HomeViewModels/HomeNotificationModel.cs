using MeetLifeClient.Models.ModelsForLiveInfo;
using MeetLifeClient.Models.ModelsForLiveInfo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models.HomeViewModels
{
    public class HomeNotificationModel
    {
        public int CountNoSeenNotification { get; set; }

        public IEnumerable<INotificationOnUser> Notification { get; set; }
    }
}