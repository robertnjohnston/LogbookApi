using System.Web.Http;
using Swashbuckle.Application;

namespace LogbookApi
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("V1", "Logbook API");
            });
        }
    }
}