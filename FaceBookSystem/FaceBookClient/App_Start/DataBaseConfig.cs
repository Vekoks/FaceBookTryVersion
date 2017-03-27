using FaceBook.Data;
using FaceBook.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FaceBookClient.App_Start
{
    public class DataBaseConfig
    {
        public static void Init()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FaceBookDbContext, Configuration>());

            var db = new FaceBookDbContext();
        }
    }
}