//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LogbookApi.Database
{
    using System;
    
    public partial class GetLastFlightDetails_Result
    {
        public int FlightNumber { get; set; }
        public System.DateTime FlightDate { get; set; }
        public int AircraftId { get; set; }
        public string Aircraft { get; set; }
        public int AirfieldId { get; set; }
        public string Airfield { get; set; }
        public string AircraftReg { get; set; }
        public string LaunchType { get; set; }
        public int DurationMin { get; set; }
        public string Duration { get; set; }
        public Nullable<int> DistanceFlown { get; set; }
        public Nullable<bool> Declared { get; set; }
        public int PilotInCharge { get; set; }
        public string Notes { get; set; }
        public string TraceFile { get; set; }
        public Nullable<int> PageNumber { get; set; }
    }
}
