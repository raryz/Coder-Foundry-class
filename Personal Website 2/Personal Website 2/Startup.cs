using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Personal_Website_2.Startup))]
namespace Personal_Website_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
