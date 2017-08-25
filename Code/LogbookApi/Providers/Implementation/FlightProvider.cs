using System;
using System.Collections.Generic;
using LogbookApi.Models;
using CuttingEdge.Conditions;

namespace LogbookApi.Providers.Implementation
{
    public class FlightProvider : IFlightProvider
    {
        private readonly jetstrea_LogbookEntities _context;

        public FlightProvider(jetstrea_LogbookEntities context)
        {
            Condition.Requires(context, nameof(context)).IsNotNull();
            _context = context;
        }

        public List<Flight> GetFilteredFlights(FlightFilter filter)
        {
            throw new System.NotImplementedException();
        }

        public Flight GetFlight(int id)
        {
            throw new System.NotImplementedException();
        }

        public void SaveFlight(Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}