using MeetLifeClient.Hubs;
using MeetLifeClient.Models.ModelsForLiveInfo.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models.ModelsForLiveInfo
{
    public class NotificationOnUser : INotificationOnUser
    {
        public int NotificationId { get; set; }

        public string Description { get; set; }

        public int PostId { get; set; }

        public string Username { get; set; }

        public bool IsSaw { get; set; }

        public IEnumerable<INotificationOnUser> GetDataForNotofiactionsOnUser(string IdOfLoggedUser)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetLifeSystem"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Id]
                ,[UserName]
                ,[Disctription]
                ,[PostId]
                ,[IsSaw]
                FROM [dbo].[Notifications] WHERE UserId = @ID;", connection))
                {
                    command.Parameters.Add("@ID", SqlDbType.NVarChar);
                    command.Parameters["@ID"].Value = IdOfLoggedUser;
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChangeNotofiaction);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new NotificationOnUser()
                            {
                                NotificationId = x.GetInt32(0),
                                Username = x.GetString(1),
                                Description = x.GetString(2),
                                PostId = x.GetInt32(3),
                                IsSaw = x.GetBoolean(4)
                            }).ToList();
                }
            }
        }

        private void dependency_OnChangeNotofiaction(object sender, SqlNotificationEventArgs e)
        {
            NotificationsHub.Show();
        }
    }
}