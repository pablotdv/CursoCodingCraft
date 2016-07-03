using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exercicio02ScaffoldLayouts.Startup))]
namespace Exercicio02ScaffoldLayouts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
