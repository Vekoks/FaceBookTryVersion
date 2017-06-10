using FaceBookClient.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FaceBookClient.Models
{
    public class AskFriendInfo
    {
        public string Name { get; set; }

        public IEnumerable<AskFriendInfo> GetData(string UserIdOfLoggedUser)
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FaceBookSystem"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Username]
               FROM [dbo].[InvitationForFriends] WHERE UserId = @ID;", connection))
                {
                    command.Parameters.Add("@ID", SqlDbType.NVarChar);
                    command.Parameters["@ID"].Value = UserIdOfLoggedUser;
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new AskFriendInfo()
                            {
                                Name = x.GetString(0)
                            }).ToList();
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            AskFriendHub.Show();
        }
    }
}