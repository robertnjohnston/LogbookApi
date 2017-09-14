using System;
using System.Collections.Generic;
using LogbookApi.Models;
using CuttingEdge.Conditions;

namespace LogbookApi.Providers.Implementation
{
    public class FlightProvider : IFlightProvider
    {
        private readonly jetstrea_LogbookEntities _context;
        private readonly IEntityProvider<Airfield> _airfieldProvider;
        private readonly IEntityProvider<Aircraft> _aircraftProvider;

        public FlightProvider(jetstrea_LogbookEntities context, IEntityProvider<Airfield> airfieldProvider, IEntityProvider<Aircraft> aircraftProvider)
        {
            Condition.Requires(context, nameof(context)).IsNotNull();
            Condition.Requires(airfieldProvider, nameof(airfieldProvider)).IsNotNull();
            Condition.Requires(aircraftProvider, nameof(aircraftProvider)).IsNotNull();
            
            _context = context;
            _airfieldProvider = airfieldProvider;
            _aircraftProvider = aircraftProvider;
        }

        public List<Flight> GetFilteredFlights(FlightFilter filter)
        {
            throw new System.NotImplementedException();
        }

        public Flight GetFlight(int id)
        {
            var flightExists = _context.Flight.Find(id);
            if(flightExists != null)
            {
                flightExists.Airfield = _airfieldProvider.Get(flightExists.AirfieldId)?.Name;
                flightExists.Aircraft = _aircraftProvider.Get(flightExists.AircraftId)?.Name;
            }

            return flightExists;
        }
        
        public void SaveFlight(Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}