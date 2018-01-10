using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeanERP.Startup))]
namespace DeanERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
