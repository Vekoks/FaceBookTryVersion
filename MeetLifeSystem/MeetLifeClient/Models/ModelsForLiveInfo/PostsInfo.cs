using MeetLifeClient.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models.ModelsForLiveInfo
{
    public class PostsInfo : IPostsInfo
    {
        public DateTime DateOnPost { get; set; }

        public string Description { get; set; }

        public byte[] ImagePost { get; set; }

        public int PostID { get; set; }

        public string UserId { get; set; }


        public IEnumerable<IPostsInfo> GetPosts()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetLifeSystem"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Id],[Disctription],[ImagePost],[DateOnPost],[UserId]
                FROM [dbo].[Posts]", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChangeUser);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new PostsInfo()
                            {
                               PostID = x.GetInt32(0),
                               Description =x.GetString(1),
                               ImagePost = (byte[])x.GetValue(2),
                               DateOnPost = x.GetDateTime(3),
                               UserId = x.GetString(4)
                            }).ToList();
                }
            }
        }
        private void dependency_OnChangeUser(object sender, SqlNotificationEventArgs e)
        {
            PostHub.Show();
        }
    }
}