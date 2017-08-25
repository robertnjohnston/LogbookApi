using System.Web.Http;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.WebApi;
using Owin;

namespace LogbookApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration {IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly};
            
            httpConfiguration.MapHttpAttributeRoutes();

            IUnityContainer unityContainer = new UnityContainer().RegisterWebDependencies();
            httpConfiguration.DependencyResolver = new UnityDependencyResolver(unityContainer);
            
        }
    }
}