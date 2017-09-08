using Owin;
using System.Web.Http;
using System.Web.Mvc;

namespace SampleBlog.Tests
{
    /// <summary>
    /// Configuration of self-hosted HTTP server used for integration testing
    /// </summary>
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            NinjectWebCommon.Start();

            configuration.DependencyResolver = NinjectWebCommon.Resolver;
            configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            // Execute any other ASP.NET Web API-related initialization, i.e. IoC, authentication, logging, mapping, DB, etc.
            app.UseWebApi(configuration);
            configuration.EnsureInitialized();
        }

    }
}
