using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models
{
    public static class Converters
    {
        public static string ConvertByteArrToStringForImg(byte[] arr)
        {
            var base64 = Convert.ToBase64String(arr);
            var srcImg = string.Format("data:image/gif;base64,{0}", base64);

            return srcImg;
        }

        public static string CreateStringDate(DateTime DateOnThePost)
        {
            var time = DateTime.Now - DateOnThePost;

            if (time.Days > 0)
            {
                return DateOnThePost.Day + " " +  Converters.TakeMonth(DateOnThePost.Month) + " in " + DateOnThePost.Hour + ":" + DateOnThePost.Minute;
            }

            var resoultTime = time.Hours.ToString() + ":" + time.Minutes;

            return resoultTime;
        }

        public static string TakeMonth(int NumberOfMonth)
        {
            switch (NumberOfMonth)
            {
                case 1: return "January";
                case 2: return "February";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "August";
                case 9: return "September";
                case 10: return "October";
                case 11: return "November";
                case 12: return "December";
                default:
                    return "";
            }
        }
    }
}