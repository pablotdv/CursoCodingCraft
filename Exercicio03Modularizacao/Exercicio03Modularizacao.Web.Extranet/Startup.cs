using Owin;

namespace Exercicio03Modularizacao.Web.Extranet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
