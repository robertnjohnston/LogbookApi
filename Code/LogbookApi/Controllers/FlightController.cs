using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CuttingEdge.Conditions;
using LogbookApi.Models;
using LogbookApi.Providers;

namespace LogbookApi.Controllers
{
    [RoutePrefix("api/flight")]
    public class FlightController : ApiController
    {
        private readonly IFlightProvider _flightProvider;

        //private readonly IPageProvider _pageProvider;
        public FlightController(IFlightProvider flightProvider)
        {
            Condition.Requires(flightProvider, nameof(flightProvider)).IsNotNull();

            _flightProvider = flightProvider;
        }

        [HttpGet]
        [Route("/FlightById/{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return NotFound();

            dynamic flight = _flightProvider.GetFlight(id);

            return (flight == null) ? NotFound() : Ok(flight);
        }

        [HttpGet]
        [Route("/FlightByFilter")]
        public IHttpActionResult Get([FromBody] FlightFilter filter)
        {
            dynamic flights = _flightProvider.GetFilteredFlights(filter);

            return ((List<Flight>)flights).Any() ? Ok(flights) : NotFound();
        }

        [HttpPost]
        public IHttpActionResult Post(Flight flight)
        {
            if (flight == null)
                return BadRequest();
            
            _flightProvider.SaveFlight(flight);
            return Created("ok", flight);
        }
    }
}