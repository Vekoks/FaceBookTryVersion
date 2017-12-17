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

        public IEnumerable<ListNotification> Notification { get; set; }
    }

    public class ListNotification
    {
        public int NotificationId { get; set; }

        public string Username { get; set; }

        public string ImgUser { get; set; }

        public string Description { get; set; }

        public int PostId { get; set; }

        public bool IsSaw { get; set; }
    }
}