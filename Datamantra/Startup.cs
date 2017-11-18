using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Datamantra.Startup))]
namespace Datamantra
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
