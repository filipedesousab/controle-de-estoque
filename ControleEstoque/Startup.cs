using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ControleEstoque.Startup))]
namespace ControleEstoque
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
