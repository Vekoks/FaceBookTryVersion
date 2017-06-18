﻿using FaceBookClient.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FaceBookClient.Models
{
    public class UsersInfo : IUsersInfo
    {
        public string Name { get; set; }

        public bool IsOnline { get; set; }

        public IEnumerable<IUsersInfo> GetData()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FaceBookSystem"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [UserName],[IsOnline]
               FROM [dbo].[AspNetUsers]", connection))
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
                            .Select(x => new UsersInfo()
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