using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(Breaking.AspNet.Startup))]

namespace Breaking.AspNet
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = new []{ "index.html" }
            });

            app.UseStaticFiles();
        }
    }
}
