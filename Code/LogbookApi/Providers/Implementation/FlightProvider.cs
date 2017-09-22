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
            if (!filter.IsValid()) throw new InvalidFilterException("");
            
            var flights = GetAllFlights();

            if(((int)filter.FilterType & (int)FilterType.Number) == (int)FilterType.Number) flights = GetFlightsByNumber(flights, filter);
            if(((int)filter.FilterType & (int)FilterType.Date) == (int)FilterType.Date) flights = GetFlightsByDate(flights, filter);
            if(((int)filter.FilterType & (int)FilterType.Aircraft) == (int)FilterType.Aircraft) flights = GetFlightByAircraft(flights, filter);
            if(((int)filter.FilterType & (int)FilterType.Airfield) == (int)FilterType.Airfield) flights = GetFlightByAirfield(flights, filter);
            if(((int)filter.FilterType & (int)FilterType.Launch) == (int)FilterType.Launch) flights = GetFlightByLaunch(flights, filter);
            if(((int)filter.FilterType & (int)FilterType.Crew) == (int)FilterType.Crew) flights = GetFlightByCrew(flights, filter);
            if(((int)filter.FilterType & (int)FilterType.Trace) == (int)FilterType.Trace) flights = GetFlightsWithTraceFiles(flights);

            flights.ForEach(GetAirfieldAndAircraft);

            return flights;
        }

        public Flight GetFlight(int id)
        {
            var flightExists = _context.Flight.Find(id);
            if(flightExists != null)
            {
                GetAirfieldAndAircraft(flightExists);
            }

            return flightExists;
        }
        
        public Flight SaveFlight(Flight flight)
        {
            Condition.Requires(flight, nameof(flight)).IsNotNull();

            if (!flight.IsValid()) throw new InvalidFlightException(flight.Error);

            if (flight.AircraftId == 0)
            {
                flight.AircraftId = _aircraftProvider.Save(new Aircraft { Name = flight.Aircraft }).Id;
            }
            if (flight.AirfieldId == 0)
            {
                flight.AirfieldId = _airfieldProvider.Save(new Airfield { Name = flight.Airfield }).Id;
            }

            if (flight.FlightNumber == 0)
            {
                flight.FlightNumber = GetLastFlightNumber() + 1;
                _context.Flight.Add(flight);
            }
            else
            {
                var flightToUpdate = _context.Flight.Find(flight.FlightNumber);
                _context.Entry(flightToUpdate).CurrentValues.SetValues(flight);
            }

            _context.SaveChanges();
            return flight;
        }

        public int GetLastFlightNumber()
        {
            return _context.Flight.Max(flight => flight.FlightNumber);
        }

        private List<Flight> GetFlightsWithTraceFiles(IEnumerable<Flight> flights)
        {
            return flights.Where(flight => _context.Trace.Any(trace => trace.FlightNumber == flight.FlightNumber)).ToList();
        }

        private List<Flight> GetFlightByCrew(IEnumerable<Flight> flights, FlightFilter filter)
        {
            return flights.Where(flight => flight.PilotInCharge == filter.Crew).ToList();
        }

        private List<Flight> GetFlightByLaunch(IEnumerable<Flight> flights, FlightFilter filter)
        {
            return flights.Where(flight => flight.LaunchType.ToLower() == filter.Launch.ToLower()).ToList();
        }

        private List<Flight> GetFlightByAirfield(IEnumerable<Flight> flights, FlightFilter filter)
        {
            return flights.Where(flight => flight.AirfieldId == filter.Airfield).ToList();
        }
        private List<Flight> GetFlightByAircraft(IEnumerable<Flight> flights, FlightFilter filter)
        {
            return flights.Where(flight => flight.AircraftId == filter.Aircraft).ToList();
        }

        private List<Flight> GetFlightsByDate(IEnumerable<Flight> flights, FlightFilter filter)
        {
            return flights.Where(flight => flight.FlightDate >= filter.DateStart && flight.FlightDate <= filter.DateEnd).ToList();
        }

        private List<Flight> GetFlightsByNumber(IEnumerable<Flight> flights, FlightFilter filter)
        {
            return flights.Where(flight => flight.FlightNumber >= filter.FlightStart && flight.FlightNumber <= filter.FlightEnd).ToList();
        }

        private List<Flight> GetAllFlights()
        {
            return _context.Flight.ToList();
        }

        private void GetAirfieldAndAircraft(Flight flight)
        {
            flight.Airfield = _airfieldProvider.Get(flight.AirfieldId)?.Name;
            flight.Aircraft = _aircraftProvider.Get(flight.AircraftId)?.Name;
        }
    }
}