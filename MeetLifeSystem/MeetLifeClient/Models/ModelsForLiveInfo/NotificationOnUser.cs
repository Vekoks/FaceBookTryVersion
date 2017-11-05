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
    public class NotificationOnUser : INotificationOnUser
    {
        public string Description { get; set; }

        public int PostId { get; set; }

        public string Username { get; set; }

        public IEnumerable<INotificationOnUser> GetDataForNotofiactionsOnUser(string IdOfLoggedUser)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetLifeSystem"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [UserName]
                ,[Disctription]
                ,[PostId]
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
                                Username = x.GetString(0),
                                Description = x.GetString(1),
                                PostId = x.GetInt32(2)
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