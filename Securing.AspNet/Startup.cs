using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Securing.AspNet.Startup))]
namespace Securing.AspNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
