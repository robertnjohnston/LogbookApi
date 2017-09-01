using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using FluentAssertions;
using LogbookApi;
using LogbookApi.Controllers;
using LogbookApi.Models;
using LogbookApi.Providers;
using LogbookApiTest.TestData;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LogbookApiTest.Controllers
{
    [TestFixture]
    public class FlightControllerTests
    {
        private Mock<IFlightProvider> MockFlightProvider { get; set; }

        private Mock<IPageProvider> MockPageProvider { get; set; }

        [SetUp]
        public void SetupTests()
        {
            MockFlightProvider = new Mock<IFlightProvider>();

            MockPageProvider = new Mock<IPageProvider>();
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionOnMissingFlightProvider()
        {
            Action act = () => new FlightController(null);

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("flightProvider");
        }

        [Test]
        public void ShouldReturnAFlightController()
        {
            var fc = GetTestSubject();
            fc.Should().BeAssignableTo<FlightController>();
        }

        [Test]
        [TestCase(0)]
        [TestCase(99)]
        public void ShouldReturnNotFoundOnFlightNumberNotFound(int number)
        {
            MockFlightProvider.Setup(m => m.GetFlight(It.IsAny<int>())).Returns((Flight)null);

            var result = GetTestSubject().Get(number) as NotFoundResult;

            result.Should().NotBe(null);
        }

        [Test]
        public void ShouldReturnBadRequestOnNullFilter()
        {
            MockFlightProvider.Setup(m => m.GetFilteredFlights(It.IsAny<FlightFilter>())).Returns(new List<Flight>());

            var result = GetTestSubject().Get((FlightFilter)null) as BadRequestErrorMessageResult;

            result.Should().NotBe(null);
        }

        [Test]
        public void ShouldReturnBadRequestOnInvalidFilter()
        {
            MockFlightProvider.Setup(m => m.GetFilteredFlights(It.IsAny<FlightFilter>())).Returns(new List<Flight>());
            
            var result = GetTestSubject().Get(new FlightFilter()) as BadRequestErrorMessageResult;

            result.Should().NotBe(null);
        }

        [Test]
        public void ShouldReturnNotFoundOnFilter()
        {
            MockFlightProvider.Setup(m => m.GetFilteredFlights(It.IsAny<FlightFilter>())).Returns(new List<Flight>());

            var result = GetTestSubject().Get(new FlightFilter {FilterType = FilterType.Airfield, Airfield = 99}) as NotFoundResult;

            result.Should().NotBe(null);
        }

        [Test]
        public void ShouldReturnListOfFlightsOnFilter()
        {
            MockFlightProvider.Setup(m => m.GetFilteredFlights(It.IsAny<FlightFilter>())).Returns(FlightTestData.FilteredFlights());

            var result = GetTestSubject().Get(new FlightFilter { FilterType = FilterType.Number, FlightStart = 1, FlightEnd = 3}) as OkNegotiatedContentResult<List<Flight>>;

            GetContent<List<Flight>>(result).Count.Should().Be(3);
        }

        [Test]
        public void ShouldReturnOneFlight()
        {
            MockFlightProvider.Setup(m => m.GetFlight(It.IsAny<int>())).Returns(FlightTestData.Flight(3));

            var result = GetTestSubject().Get(3) as OkNegotiatedContentResult<Flight>;

            result.Content.ShouldBeEquivalentTo(FlightTestData.Flight(3));
        }

        [Test]
        public void ShouldReturnBadRequestOnNullInput()
        {
            var result = GetTestSubject().Post((Flight)null) as BadRequestResult;

            result.Should().NotBeNull();
        }

        [Test]
        public void ShouldReturnCreatedResult()
        {
            var result = GetTestSubject().Post(FlightTestData.Flight(1)) as CreatedNegotiatedContentResult<Flight>;

            result.Should().NotBeNull();
        }

        [Test]
        public void ShouldReturnCreatedFlight()
        {
            var result = GetTestSubject().Post(FlightTestData.Flight(1)) as CreatedNegotiatedContentResult<Flight>;

            var content = GetContent<Flight>(result);

            content.ShouldBeEquivalentTo(FlightTestData.Flight(1));
        }

        private FlightController GetTestSubject()
        {
             return new FlightController(MockFlightProvider.Object)
             {
                 ControllerContext = new HttpControllerContext
                 { Configuration = new HttpConfiguration() }, Request = new HttpRequestMessage()
             };
        }

        private static T GetContent<T>(OkNegotiatedContentResult<List<Flight>> result)
        {
            var content = result?.ExecuteAsync(new CancellationToken()).Result;
            return JsonConvert.DeserializeObject<T>(content?.Content.ReadAsStringAsync().Result);
        }

        private static T GetContent<T>(CreatedNegotiatedContentResult<Flight> result)
        {
            var content = result?.ExecuteAsync(new CancellationToken()).Result;
            return JsonConvert.DeserializeObject<T>(content?.Content.ReadAsStringAsync().Result);
        }


    }
}