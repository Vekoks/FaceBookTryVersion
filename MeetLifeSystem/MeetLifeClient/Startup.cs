using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MeetLifeClient.Startup))]
namespace MeetLifeClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
