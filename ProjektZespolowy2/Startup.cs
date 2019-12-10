using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektZespolowy2.Startup))]
namespace ProjektZespolowy2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
