using System;
using System.Web.Http;
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
            ConfigureWebApi(app);
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Disable the XML formatter by removing all media types assigned to it
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            // Prettify the JSON output
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            config.MapHttpAttributeRoutes();
            
            app.UseWebApi(config);
        }
    }
}
