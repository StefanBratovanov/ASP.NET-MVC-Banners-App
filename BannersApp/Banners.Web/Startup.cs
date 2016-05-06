using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Banners.Web.Startup))]
namespace Banners.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
