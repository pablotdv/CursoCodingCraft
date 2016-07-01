using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exercicio02Layouts.Startup))]
namespace Exercicio02Layouts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
