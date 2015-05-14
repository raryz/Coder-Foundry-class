using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BudgetToolRAR.Startup))]
namespace BudgetToolRAR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
