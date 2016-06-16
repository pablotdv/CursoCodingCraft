using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GaveteiroLanches.Web.Startup))]
namespace GaveteiroLanches.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
