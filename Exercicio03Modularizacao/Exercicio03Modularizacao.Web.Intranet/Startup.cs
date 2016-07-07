using Owin;

namespace Exercicio03Modularizacao.Web.Intranet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
