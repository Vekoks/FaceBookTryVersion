using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models.HomeViewModels
{
    public class HomeChatModel
    {
        public bool IsMe { get; set; }

        public string UserName { get; set; }

        public string Letter { get; set; }

        public string Date { get; set; }
    }
}