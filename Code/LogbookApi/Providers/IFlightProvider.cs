﻿using System.Collections.Generic;
using LogbookApi.Database;
using LogbookApi.Models;

namespace LogbookApi.Providers
{
    public interface IFlightProvider
    {
        List<Flight> GetFilteredFlights(FlightFilter filter);

        Flight GetFlight(int id);

        Flight SaveFlight(Flight flight);

        int GetLastFlightNumber();

    }
}