using System;

namespace LogbookApi.Database
{
    public partial class Flight
    {
        public string Error { get; set; }

        public bool IsValid()
        {
            Error = string.Empty;

            if (FlightDate == DateTime.MinValue)
            {
                Error += (Error.Length > 0 ? Environment.NewLine : string.Empty) + "Invalid flight date";
            }
            if (AircraftId == 0 && string.IsNullOrWhiteSpace(Aircraft?.Name))
            {
                Error += (Error.Length > 0 ? Environment.NewLine : string.Empty) + "Invalid aircraft";
            }
            if (AirfieldId == 0 && string.IsNullOrWhiteSpace(Airfield?.Name))
            {
                Error += (Error.Length > 0 ? Environment.NewLine : string.Empty) + "Invalid airfield";
            }
            if (DurationMin == 0)
            {
                Error += (Error.Length > 0 ? Environment.NewLine : string.Empty) + "Invalid flight time";
            }
            if (PilotInCharge < 0 || PilotInCharge > 2)
            {
                Error += (Error.Length > 0 ? Environment.NewLine : string.Empty) + "Invalid crew position";
            }

            return Error.Length == 0;
        }
    }
}