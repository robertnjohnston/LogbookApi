using System.Collections.Generic;
using LogbookApi.Models;

namespace LogbookApi.Providers
{
    public interface IFlightProvider
    {
        List<Flight> GetFilteredFlights(FlightFilter filter);

        Flight GetFlight(int id);

        void SaveFlight(Flight flight);
    }
}