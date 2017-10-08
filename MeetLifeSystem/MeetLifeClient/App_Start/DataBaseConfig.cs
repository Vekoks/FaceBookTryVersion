using MeetLife.Data;
using MeetLife.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeetLifeClient.App_Start
{
    public class DataBaseConfig
    {
        public static void Init()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MeetLifeDbContext, Configuration>());
            MeetLifeDbContext.Create().Database.Initialize(true);
        }
    }
}