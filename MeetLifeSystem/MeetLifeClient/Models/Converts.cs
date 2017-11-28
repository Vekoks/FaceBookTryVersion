using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models
{
    public static class Converts
    {
        public static string ConvertByteArrToStringForImg(byte[] arr)
        {
            var base64 = Convert.ToBase64String(arr);
            var srcImg = string.Format("data:image/gif;base64,{0}", base64);

            return srcImg;
        }
    }
}