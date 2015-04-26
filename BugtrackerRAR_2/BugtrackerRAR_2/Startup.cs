using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BugtrackerRAR_2.Startup))]
namespace BugtrackerRAR_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
