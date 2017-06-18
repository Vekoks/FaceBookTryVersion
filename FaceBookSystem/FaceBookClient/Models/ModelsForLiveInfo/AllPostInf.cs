using FaceBook.Model;
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
    public class AllPostInf
    {
        public string Discription { get; set; }

        public DateTime DatePost { get; set; }

        public string UserId { get; set; }

        public IEnumerable<AllPostInf> GetDataAllPost()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FaceBookSystem"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Disctription],[DateOnPost],[UserId]
               FROM [dbo].[Posts]", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new AllPostInf()
                            {
                                Discription = x.GetString(0),
                                DatePost = x.GetDateTime(1),
                                UserId = x.GetString(2),
                            }).ToList();
                }
            }
        }
        
        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            PostHub.Show();
        }
    }
}