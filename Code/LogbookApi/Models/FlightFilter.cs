﻿using System;

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
            if(FilterType == (FilterType.Date & FilterType.Number) || FilterType==FilterType.None) return false;

            var result = true;

            if(((int) FilterType & (int) FilterType.Number) == (int) FilterType.Number) result = ValidateNumbers();
            if(((int) FilterType & (int) FilterType.Date) == (int) FilterType.Date) result = ValidateDates();
            if(((int) FilterType & (int) FilterType.Aircraft) == (int) FilterType.Aircraft) result = ValidateId(Aircraft);
            if(((int) FilterType & (int) FilterType.Airfield) == (int) FilterType.Airfield) result = ValidateId(Airfield);
            if(((int) FilterType & (int) FilterType.Launch) == (int) FilterType.Launch) result = ValidateLaunch();
            if(((int) FilterType & (int) FilterType.Crew) == (int) FilterType.Crew) result = ValidateCrew();
            if(((int) FilterType & (int) FilterType.Trace) == (int) FilterType.Trace) TraceFile = true;

            return result;
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