using FaceBookClient.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FaceBookClient.Models.ModelsForLiveInfo
{
    public class NoSeenMessage : INoSeenMessage
    {
        public string FormUser { get; set; }

        public string Message { get; set; }

        public IEnumerable<INoSeenMessage> GetDataForMessage(string UserIdOfLoggedUser)
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FaceBookSystem"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [UserName], [Letter]
                FROM [dbo].[Messages] WHERE UserId = @ID;", connection))
                {
                    command.Parameters.Add("@ID", SqlDbType.NVarChar);
                    command.Parameters["@ID"].Value = UserIdOfLoggedUser;
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChangeNoSeenMessage);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new NoSeenMessage()
                            {
                                FormUser = x.GetString(0),
                                Message = x.GetString(1)
                            }).ToList();
                }
            }
        }

        private void dependency_OnChangeNoSeenMessage(object sender, SqlNotificationEventArgs e)
        {
            NoSeenMessageHub.Show();
        }
    }
}