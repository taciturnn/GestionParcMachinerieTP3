using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GestionParcMachinerieTP3.Startup))]
namespace GestionParcMachinerieTP3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
