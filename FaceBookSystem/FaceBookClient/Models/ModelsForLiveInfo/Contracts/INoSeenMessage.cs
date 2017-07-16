﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBookClient.Models.ModelsForLiveInfo
{
    public interface INoSeenMessage
    {
        string FormUser { get; set; }

        string Message { get; set; }

        IEnumerable<INoSeenMessage> GetDataForMessage(string UserIdOfLoggedUser);
    }
}
