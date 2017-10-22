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
    public class LikeOnPost : ILikeOnPost
    {
        public int PostId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<LikeOnPost> GetDataLikesOnThePost(int PostId)
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetLifeSystem"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Username],[PostId]
                FROM [dbo].[LikesOnPosts] WHERE PostId = @ID;", connection))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int);
                    command.Parameters["@ID"].Value = PostId;
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChangeLike);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new LikeOnPost()
                            {
                                UserName = x.GetString(0),
                                PostId = x.GetInt32(1),
                            }).ToList();
                }
            }
        }

        private void dependency_OnChangeLike(object sender, SqlNotificationEventArgs e)
        {
            LikeHub.Show();
        }
    }
}