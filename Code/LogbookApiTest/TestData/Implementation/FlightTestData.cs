using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LogbookApi;

namespace LogbookApiTest.TestData.Implementation
{
    public static class FlightTestData
    {
        public static readonly List<Flight> FlightData = new List<Flight>
        {
            new Flight {FlightNumber = 1,FlightDate = DateTime.Parse("1988-01-30 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 15,DistanceFlown = 0,Declared = false,PilotInCharge = 2},
            new Flight {FlightNumber = 2,FlightDate = DateTime.Parse("1988-02-06 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 15,DistanceFlown = 0,Declared = false,PilotInCharge = 2},
            new Flight {FlightNumber = 3,FlightDate = DateTime.Parse("1988-02-06 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 15,DistanceFlown = 0,Declared = false,PilotInCharge = 2},
            new Flight {FlightNumber = 4,FlightDate = DateTime.Parse("1988-02-20 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 12,DistanceFlown = 0,Declared = false,PilotInCharge = 2},
            new Flight {FlightNumber = 5,FlightDate = DateTime.Parse("1988-03-05 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 23,DistanceFlown = 0,Declared = false,PilotInCharge = 2},
            new Flight {FlightNumber = 6,FlightDate = DateTime.Parse("1988-03-20 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 11,DistanceFlown = 0,Declared = false,PilotInCharge = 2},
            new Flight {FlightNumber = 7,FlightDate = DateTime.Parse("1988-03-20 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 16,DistanceFlown = 0,Declared = false,PilotInCharge = 2},
            new Flight {FlightNumber = 8,FlightDate = DateTime.Parse("1988-04-02 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 15,DistanceFlown = 0,Declared = false,PilotInCharge = 2},
            new Flight {FlightNumber = 9,FlightDate = DateTime.Parse("1988-04-02 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 10,DistanceFlown = 0,Declared = false,PilotInCharge = 2},
            new Flight {FlightNumber = 10,FlightDate = DateTime.Parse("1988-04-02 00:00:00.000"),AirfieldId = 1, AircraftId  = 1, AircraftReg = "BVS", LaunchType = "A",DurationMin = 15,DistanceFlown = 0,Declared = false,PilotInCharge = 2},
            new Flight {FlightNumber = 11,FlightDate = DateTime.Parse("1992-01-30 00:00:00.000"),AirfieldId = 2, AircraftId  = 2, AircraftReg = "CFC", LaunchType = "A",DurationMin = 35,DistanceFlown = 0,Declared = false,PilotInCharge = 1},
            new Flight {FlightNumber = 12,FlightDate = DateTime.Parse("1992-02-06 00:00:00.000"),AirfieldId = 2, AircraftId  = 2, AircraftReg = "CFC", LaunchType = "A",DurationMin = 25,DistanceFlown = 0,Declared = false,PilotInCharge = 1},
            new Flight {FlightNumber = 13,FlightDate = DateTime.Parse("1992-02-06 00:00:00.000"),AirfieldId = 2, AircraftId  = 2, AircraftReg = "CFC", LaunchType = "A",DurationMin = 15,DistanceFlown = 0,Declared = false,PilotInCharge = 1},
            new Flight {FlightNumber = 14,FlightDate = DateTime.Parse("1992-02-20 00:00:00.000"),AirfieldId = 2, AircraftId  = 2, AircraftReg = "CFC", LaunchType = "A",DurationMin = 22,DistanceFlown = 0,Declared = false,PilotInCharge = 1},
            new Flight {FlightNumber = 15,FlightDate = DateTime.Parse("1992-03-05 00:00:00.000"),AirfieldId = 2, AircraftId  = 2, AircraftReg = "CFC", LaunchType = "A",DurationMin = 43,DistanceFlown = 0,Declared = false,PilotInCharge = 1},
            new Flight {FlightNumber = 16,FlightDate = DateTime.Parse("1992-03-20 00:00:00.000"),AirfieldId = 2, AircraftId  = 2, AircraftReg = "CFC", LaunchType = "A",DurationMin = 51,DistanceFlown = 0,Declared = false,PilotInCharge = 1},
            new Flight {FlightNumber = 17,FlightDate = DateTime.Parse("2008-03-20 00:00:00.000"),AirfieldId = 3, AircraftId  = 3, AircraftReg = "EMF", LaunchType = "A",DurationMin = 76,DistanceFlown = 0,Declared = false,PilotInCharge = 1},
            new Flight {FlightNumber = 18,FlightDate = DateTime.Parse("2008-04-02 00:00:00.000"),AirfieldId = 3, AircraftId  = 3, AircraftReg = "EMF", LaunchType = "A",DurationMin = 115,DistanceFlown = 0,Declared = false,PilotInCharge = 1},
            new Flight {FlightNumber = 19,FlightDate = DateTime.Parse("2008-04-02 00:00:00.000"),AirfieldId = 3, AircraftId  = 3, AircraftReg = "EMF", LaunchType = "A",DurationMin = 110,DistanceFlown = 0,Declared = false,PilotInCharge = 1},
            new Flight {FlightNumber = 20,FlightDate = DateTime.Parse("2008-04-02 00:00:00.000"),AirfieldId = 3, AircraftId  = 3, AircraftReg = "EMF", LaunchType = "A",DurationMin = 150,DistanceFlown = 0,Declared = false,PilotInCharge = 1}
        };

        public static readonly List<Aircraft> AircraftData = new List<Aircraft>
                                                             {
                                                                 new Aircraft {Id = 1, Name = "Bocian"},
                                                                 new Aircraft {Id = 2, Name = "Pirat"},
                                                                 new Aircraft {Id = 3, Name = "LS4"}
                                                             };

        public static readonly List<Airfield> AirfieldData = new List<Airfield>
                                                             {
                                                                 new Airfield {Id = 1, Name = "Cranfield"},
                                                                 new Airfield {Id = 2, Name = "Hinton"},
                                                                 new Airfield {Id = 3, Name = "Dunstable"}
                                                             };

        public static readonly List<Page> PageData = new List<Page>
                                                     {
                                                         new Page{ PageNumber = 1, StartFlight = 1, EndFlight = 10, P1MFlight = 0, P1MMin = 0, P2Flight = 10, P2Min = 147, P1SFlight = 0, P1SMin = 0, Flights = FlightData.GetRange(0,9)},
                                                         new Page{ PageNumber = 1, StartFlight = 11, EndFlight = 16, P1MFlight = 0, P1MMin = 0, P2Flight = 10, P2Min = 147, P1SFlight = 0, P1SMin = 0, Flights = FlightData.GetRange(10,6)},
                                                         new Page{ PageNumber = 1, StartFlight = 17, EndFlight = 20, P1MFlight = 0, P1MMin = 0, P2Flight = 10, P2Min = 147, P1SFlight = 0, P1SMin = 0, Flights = FlightData.GetRange(16,4)}
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
                       Flights = FlightData.Where(flight => flight.FlightNumber > 0 && flight.FlightNumber <11).ToList()
                   };
        }

        public static Flight Flight(int id)
        {
            return FlightData[id];
        }

        public static List<Flight> FilteredFlights()
        {
            return FlightData.Where(f => f.FlightNumber > 3 && f.FlightNumber < 7).ToList();
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
            return new ObservableCollection<Flight>(FlightData);
        }

        public static ObservableCollection<Aircraft> Aircrafts()
        {
            return new ObservableCollection<Aircraft>(AircraftData);
        }

        public static ObservableCollection<Airfield> Airfields()
        {
            return new ObservableCollection<Airfield>(AirfieldData);
        }

        public static ObservableCollection<Page> Pages()
        {
            return new ObservableCollection<Page>(PageData);
        }
    }
}