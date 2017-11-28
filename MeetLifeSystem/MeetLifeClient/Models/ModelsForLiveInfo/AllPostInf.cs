using MeetLifeClient.Hubs;
using MeetLifeClient.Models.ModelsForLiveInfo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models
{
    public class AllPostInf : IAllPostInfo
    {
        public int PostId { get; set; }

        public string Discription { get; set; }

        public DateTime DatePost { get; set; }

        public object PictureId { get; set; }

        public string UserId { get; set; }

        public IEnumerable<IAllPostInfo> GetDataAllPost()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetLifeSystem"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Id],[Disctription],[DateOnPost],[PictureId],[UserId]
               FROM [dbo].[Posts]", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChangePost);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new AllPostInf()
                            {
                                PostId = x.GetInt32(0),
                                Discription = x.GetString(1),
                                DatePost = x.GetDateTime(2),
                                PictureId = x.GetValue(3),
                                UserId = x.GetString(4),
                            }).ToList();
                }
            }
        }
        
        private void dependency_OnChangePost(object sender, SqlNotificationEventArgs e)
        {
            PostHub.Show();
        }
    }
}