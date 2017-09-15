using System;
using System.Collections.Generic;
using System.Linq;
using LogbookApi.Models;
using CuttingEdge.Conditions;
using LogbookApi.Exceptions;

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

            if (!filter.IsValid()) throw new InvalidFilterException();
            switch (filter.FilterType)
            {
                case FilterType.Number:
                    return GetFlightsByNumber(filter.FlightStart, filter.FlightEnd);
                case FilterType.Date:
                    return GetFlightsByDate(filter.DateStart, filter.DateEnd);
                case FilterType.Aircraft:
                    return GetFlightByAircraft(filter.Aircraft);
                case FilterType.Airfield:
                    break;
                case FilterType.Launch:
                    break;
                case FilterType.Crew:
                    break;
                case FilterType.Trace:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new List<Flight>();
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

        private List<Flight> GetFlightByAircraft(int filterAircraftId)
        {
            return _context.Flight.Where(flight => flight.AircraftId == filterAircraftId).ToList();
        }

        private List<Flight> GetFlightsByDate(DateTime filterDateStart, DateTime filterDateEnd)
        {
            return _context.Flight.Where(flight => 
                flight.FlightDate >= filterDateStart && flight.FlightDate <= filterDateEnd).ToList();
        }

        private List<Flight> GetFlightsByNumber(int filterFlightStart, int filterFlightEnd)
        {
            return _context.Flight.Where(flight =>
                flight.FlightNumber >= filterFlightStart && flight.FlightNumber <= filterFlightEnd).ToList();
        }
    }
}