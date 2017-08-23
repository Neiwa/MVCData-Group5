using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCData_Group5.Startup))]
namespace MVCData_Group5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
