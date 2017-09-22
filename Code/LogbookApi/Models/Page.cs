using System.Collections.Generic;

namespace LogbookApi
{
    public partial class Page
    {
        public List<Flight> Flights { get; set; }

        public string Error { get; set; }

        public bool IsValid()
        {
            Error = string.Empty;

            return Error.Length == 0;
        }
    }
}