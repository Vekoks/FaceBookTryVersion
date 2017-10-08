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
    public class CommentOnThePost : ICommentOnThePost
    {
        public string Username { get; set; }

        public string Description { get; set; }

        public IEnumerable<ICommentOnThePost> GetDataForCommentsOnThePost(int PostId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FaceBookSystem"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Username],[Description]
               FROM [dbo].[CommendsOnPosts] WHERE PostId = @ID;", connection))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int);
                    command.Parameters["@ID"].Value = PostId;
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChangeCommentOnThePost);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new CommentOnThePost()
                            {
                                Username = x.GetString(0),
                                Description = x.GetString(1)
                            }).ToList();
                }
            }
        }

        private void dependency_OnChangeCommentOnThePost(object sender, SqlNotificationEventArgs e)
        {
            CommentsPostHub.Show();
        }
    }
}