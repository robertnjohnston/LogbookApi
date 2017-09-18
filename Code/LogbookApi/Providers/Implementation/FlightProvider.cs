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
                    return GetFlightByAirfield(filter.Airfield);
                case FilterType.Launch:
                    return GetFlightByLaunch(filter.Launch);
                case FilterType.Crew:
                    return GetFlightByCrew(filter.Crew);
                default:
                    return new List<Flight>();
            }
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

        private List<Flight> GetFlightByCrew(int filterCrew)
        {
            var flights = _context.Flight.Where(flight => flight.PilotInCharge == filterCrew).ToList();

            flights.ForEach(GetAirfieldAndAircraft);
            return flights;
        }

        private List<Flight> GetFlightByLaunch(string filterLaunch)
        {
            var flights = _context.Flight.Where(flight => flight.LaunchType.ToLower() == filterLaunch.ToLower()).ToList();

            flights.ForEach(GetAirfieldAndAircraft);
            return flights;
        }

        private List<Flight> GetFlightByAirfield(int filterAirfieldId)
        {
            var flights = _context.Flight.Where(flight => flight.AirfieldId == filterAirfieldId).ToList();

            flights.ForEach(GetAirfieldAndAircraft);
            return flights;
        }
        private List<Flight> GetFlightByAircraft(int filterAircraftId)
        {
            var flights = _context.Flight.Where(flight => flight.AircraftId == filterAircraftId).ToList();

            flights.ForEach(GetAirfieldAndAircraft);
            return flights;
        }

        private List<Flight> GetFlightsByDate(DateTime filterDateStart, DateTime filterDateEnd)
        {
            var flights =  _context.Flight.Where(flight => 
                flight.FlightDate >= filterDateStart && flight.FlightDate <= filterDateEnd).ToList();

            flights.ForEach(GetAirfieldAndAircraft);
            return flights;
        }

        private List<Flight> GetFlightsByNumber(int filterFlightStart, int filterFlightEnd)
        {
            var flights = _context.Flight.Where(flight =>
                flight.FlightNumber >= filterFlightStart && flight.FlightNumber <= filterFlightEnd).ToList();

            flights.ForEach(GetAirfieldAndAircraft);
            return flights;
        }

        private void GetAirfieldAndAircraft(Flight flight)
        {
            flight.Airfield = _airfieldProvider.Get(flight.AirfieldId)?.Name;
            flight.Aircraft = _aircraftProvider.Get(flight.AircraftId)?.Name;
        }
    }
}