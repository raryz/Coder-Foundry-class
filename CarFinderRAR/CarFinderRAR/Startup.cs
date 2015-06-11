using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarFinderRAR.Startup))]
namespace CarFinderRAR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
