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
                .RegisterType<IEntityProvider<Airfield>, AirfieldProvider>()
                .RegisterType<IEntityProvider<Aircraft>, AircraftProvider>()
                .RegisterInstance(typeof(jetstrea_LogbookEntities), new jetstrea_LogbookEntities())
                .RegisterType<FlightController>();
        }
    }
}