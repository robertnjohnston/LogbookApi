using LogbookApi.Controllers;
using LogbookApi.Providers;
using LogbookApi.Providers.Implementation;
using Microsoft.Practices.Unity;

namespace LogbookApi
{
    public static class WebBootstrapper
    {
        public static IUnityContainer RegisterWebDependencies(this IUnityContainer unityContainer)
        {
            return unityContainer
                .RegisterType<IFlightProvider, FlightProvider>()
                .RegisterType<IPageProvider, PageProvider>()
                .RegisterType<FlightController>();
        }
    }
}