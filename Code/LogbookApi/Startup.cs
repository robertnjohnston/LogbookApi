using System.Web.Http;
using Microsoft.Owin.Cors;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.WebApi;
using Owin;
using Swashbuckle.Application;

namespace LogbookApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration {IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly};
            
            httpConfiguration.MapHttpAttributeRoutes();

            httpConfiguration.EnableSwagger(swaggerConfiguration =>
                                            {
                                                swaggerConfiguration.UseFullTypeNameInSchemaIds();
                                                swaggerConfiguration.SingleApiVersion("V0", "Logbook API");
                                                //swaggerConfiguration.OrderActionGroupsBy(new ActionNameComparer());
                                                // Filter to remove the queryOptions get action parameter which should not show up in the swagger documentation
                                                //swaggerConfiguration.OperationFilter<PartnerOperationFilter>();
                                            })
                             .EnableSwaggerUi();

            IUnityContainer unityContainer = new UnityContainer().RegisterWebDependencies();
            httpConfiguration.DependencyResolver = new UnityDependencyResolver(unityContainer);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(httpConfiguration);
        }
    }
}