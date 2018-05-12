using System.Collections.Generic;
using LogbookApi.Models;

namespace LogbookApi.Providers
{
    public interface IFlightProvider
    {
        List<Flight> GetFilteredFlights(FlightFilter filter);

        List<Flight> GetFlightsByPage(int id);

        Flight GetFlight(int id);

        Flight SaveFlight(Flight flight);

        int GetLastFlightNumber();
    }
}