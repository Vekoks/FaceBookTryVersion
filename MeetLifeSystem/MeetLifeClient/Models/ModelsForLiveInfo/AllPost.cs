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
    public class AllPost : IAllPost
    {
        public int PostId { get; set; }

        public IEnumerable<IAllPost> GetDataAllPost()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetLifeSystem"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Id]
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
                            .Select(x => new AllPost()
                            {
                                PostId = x.GetInt32(0)
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