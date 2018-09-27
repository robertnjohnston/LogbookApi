using System.Diagnostics.CodeAnalysis;
using LogbookApi.Controllers;
using LogbookApi.Database;
using LogbookApi.Providers;
using LogbookApi.Providers.Implementation;
using Unity;

namespace LogbookApi
{
    [ExcludeFromCodeCoverage]
    public static class WebBootstrapper
    {
        public static IUnityContainer RegisterWebDependencies(this IUnityContainer unityContainer)
        {

            unityContainer.RegisterType<IFlightProvider, FlightProvider>();
            unityContainer.RegisterType<IPageProvider, PageProvider>();
            unityContainer.RegisterType<IEntityProvider<Airfield>, AirfieldProvider>();
            unityContainer.RegisterType<IEntityProvider<Aircraft>, AircraftProvider>();
            unityContainer.RegisterInstance(typeof(jetstrea_LogbookEntities), new jetstrea_LogbookEntities());
            unityContainer.RegisterType<FlightController>();
            unityContainer.RegisterType<PageController>();
            return unityContainer;
        }
    }
}