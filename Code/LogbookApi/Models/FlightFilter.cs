using System;

namespace LogbookApi.Models
{
    [Flags]
    public enum FilterType
    {
        None = 0,
        Number = 1,
        Date = 2,
        Aircraft = 4,
        Airfield = 8,
        Launch = 16,
        Crew = 32,
        Trace = 64
    }
    public class FlightFilter
    {
        public FilterType FilterType { get; set; }

        public int FlightStart { get; set; }

        public int FlightEnd { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int Aircraft { get; set; }

        public int Airfield { get; set; }

        public string Launch { get; set; }

        public int Crew { get; set; }

        public bool TraceFile { get; set; }

        public bool IsValid()
        {
            return FilterIsValid();
        }

        private bool FilterIsValid()
        {
            if(FilterType == (FilterType.Date & FilterType.Number)) return false;

            switch(FilterType)
            {
                case FilterType.None:
                    return false;
                case FilterType.Date:
                    return ValidateDates();
                case FilterType.Number:
                    return ValidateNumbers();
                case FilterType.Aircraft:
                    return ValidateId(Aircraft);
                case FilterType.Airfield:
                    return ValidateId(Airfield);
                case FilterType.Crew:
                    return ValidateCrew();
                case FilterType.Launch:
                    return ValidateLaunch();
                case FilterType.Trace:
                    TraceFile = true;
                    break;
                default:
                    return false;
            }
            return true;
        }

        private bool ValidateLaunch()
        {
            if (string.IsNullOrWhiteSpace(Launch)) return false;
            return (Launch.Length==1);
        }

        private bool ValidateCrew()
        {
            return (Crew >= 0 && Crew < 3);
        }

        private bool ValidateId(int id)
        {
            return (id > 0);
        }

        private bool ValidateNumbers()
        {
            if(FlightStart <= 0 || FlightEnd <= 0) return false;
            return FlightStart < FlightEnd;
        }

        private bool ValidateDates()
        {
            if(DateStart == DateTime.MinValue || DateEnd == DateTime.MinValue) return false;
            return DateStart <= DateEnd;
        }
    }
}