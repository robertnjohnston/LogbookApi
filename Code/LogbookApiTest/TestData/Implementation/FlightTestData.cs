using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LogbookApi;

namespace LogbookApiTest.TestData
{
    public static class FlightTestData
    {
        public static readonly List<Flight> Data = new List<Flight>
            {
                new Flight {FlightNumber = 1,FlightDate = DateTime.Parse( "1988-01-30 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 15,DistanceFlown   = 0,Declared = false,PilotInCharge = 2},
                new Flight {FlightNumber = 2,FlightDate = DateTime.Parse( "1988-02-06 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 15,DistanceFlown   = 0,Declared = false,PilotInCharge = 2},
                new Flight {FlightNumber = 3,FlightDate = DateTime.Parse( "1988-02-06 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 15,DistanceFlown   = 0,Declared = false,PilotInCharge = 2},
                new Flight {FlightNumber = 4,FlightDate = DateTime.Parse( "1988-02-20 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 12,DistanceFlown   = 0,Declared = false,PilotInCharge = 2},
                new Flight {FlightNumber = 5,FlightDate = DateTime.Parse( "1988-03-05 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 23,DistanceFlown   = 0,Declared = false,PilotInCharge = 2},
                new Flight {FlightNumber = 6,FlightDate = DateTime.Parse( "1988-03-20 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 11,DistanceFlown   = 0,Declared = false,PilotInCharge = 2},
                new Flight {FlightNumber = 7,FlightDate = DateTime.Parse( "1988-03-20 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 16,DistanceFlown   = 0,Declared = false,PilotInCharge = 2},
                new Flight {FlightNumber = 8,FlightDate = DateTime.Parse( "1988-04-02 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 15,DistanceFlown   = 0,Declared = false,PilotInCharge = 2},
                new Flight {FlightNumber = 9,FlightDate = DateTime.Parse( "1988-04-02 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 10,DistanceFlown   = 0,Declared = false,PilotInCharge = 2},
                new Flight {FlightNumber = 10,FlightDate = DateTime.Parse("1988-04-02 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 15,DistanceFlown   = 0,Declared = false,PilotInCharge = 2},
            };

        public static Page Page()
        {
            
            return new Page
                   {
                       PageNumber = 1,
                       StartFlight = 1,
                       EndFlight = 10,  
                       P1MFlight = 0,
                       P1MMin = 0,
                       P2Flight = 10,
                       P2Min = 147,
                       P1SFlight = 0,
                       P1SMin = 0,
                       Flights = Data
                   };
        }

        public static Flight Flight(int id)
        {
            return Data[id];
        }

        public static List<Flight> FilteredFlights()
        {
            return Data.Where(f => f.FlightNumber > 3 && f.FlightNumber < 7).ToList();
        }

        public static Airfield Airfield()
        {
            return new Airfield {Id = 1, Name = "Cranfield"};
        }

        public static Aircraft Aircraft()
        {
            return new Aircraft { Id = 1, Name = "Bocian"};
        }

        public static ObservableCollection<Flight> Flights()
        {
            return new ObservableCollection<Flight>(Data);
        }
    }
}