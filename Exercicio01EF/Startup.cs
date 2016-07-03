using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exercicio01EF.Startup))]
namespace Exercicio01EF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
