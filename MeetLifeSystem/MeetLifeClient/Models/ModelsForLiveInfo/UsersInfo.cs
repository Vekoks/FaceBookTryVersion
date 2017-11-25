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
    public class FriendsInfo : IFrieandsInfo
    {
        public string Name { get; set; }

        public bool IsOnline { get; set; }

        public IEnumerable<IFrieandsInfo> GetFriends(string LoggedUserId)
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetLifeSystem"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [UserName],[IsOnline]
                FROM [dbo].[AspNetUsers] where User_Id = @ID;", connection))
                {
                    command.Parameters.Add("@ID", SqlDbType.NVarChar);
                    command.Parameters["@ID"].Value = LoggedUserId;
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChangeUser);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new FriendsInfo()
                            {
                                Name = x.GetString(0),
                                IsOnline = x.GetBoolean(1)
                            }).ToList();
                }
            }
        }

        private void dependency_OnChangeUser(object sender, SqlNotificationEventArgs e)
        {
            UserHub.Show();
        }
    }
}