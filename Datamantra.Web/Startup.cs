using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Datamantra.Web.Startup))]
namespace Datamantra.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
