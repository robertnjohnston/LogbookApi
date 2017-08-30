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

        public bool IsValid()
        {
            return FilterIsValid();
        }

        private bool FilterIsValid()
        {
            if(FilterType == FilterType.None) return false;
            if(FilterType == (FilterType.Date & FilterType.Number)) return false;

            return true;
        }
    }
}