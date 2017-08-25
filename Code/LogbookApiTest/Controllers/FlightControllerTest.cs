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
    public class FlightControllerTest
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
        public void ShouldReturnNotFoundOnInvalidFlightNumber()
        {
            MockFlightProvider.Setup(m => m.GetFlight(It.IsAny<int>())).Returns((Flight)null);

            var fc = GetTestSubject();

            var result = fc.Get(0) as NotFoundResult;

            result.Should().NotBe(null);
        }

        [Test]
        public void ShouldReturnNotFoundOnFlightNumberNotFound()
        {
            MockFlightProvider.Setup(m => m.GetFlight(It.IsAny<int>())).Returns((Flight)null);

            var fc = GetTestSubject();

            var result = fc.Get(999) as NotFoundResult;

            result.Should().NotBe(null);
        }

        //[Test]
        //public void ShouldReturnOkResult()
        //{
        //    MockFlightProvider.Setup(m => m.GetFlightsByPage(1)).Returns(FlightTestData.Page());

        //    var fc = GetTestSubject();

        //    var result = fc.Get(1)  as OkNegotiatedContentResult<List<Flight>>;

        //    result.Should().NotBe(null);
        //}

        //[Test]
        //public void ShouldReturnListofFlights()
        //{
        //    MockFlightProvider.Setup(m => m.GetFlightsByPage(1)).Returns(FlightTestData.Page());

        //    var fc = GetTestSubject();

        //    var result = fc.Get(1)  as OkNegotiatedContentResult<List<Flight>>;

        //    GetContent<List<Flight>>(result).Count.Should().Be(10);
        //}

        [Test]
        public void ShouldReturnNotFoundOnFilter()
        {
            MockFlightProvider.Setup(m => m.GetFilteredFlights(It.IsAny<FlightFilter>())).Returns(new List<Flight>());

            var fc = GetTestSubject();

            var result = fc.Get(new FlightFilter()) as NotFoundResult;

            result.Should().NotBe(null);
        }

        [Test]
        public void ShouldReturnListOfFlightsOnFilter()
        {
            MockFlightProvider.Setup(m => m.GetFilteredFlights(It.IsAny<FlightFilter>())).Returns(FlightTestData.FilteredFlights());

            var fc = GetTestSubject();

            var result = fc.Get(new FlightFilter()) as OkNegotiatedContentResult<List<Flight>>;

            GetContent<List<Flight>>(result).Count.Should().Be(3);
        }

        [Test]
        public void ShouldReturnOneFlight()
        {
            MockFlightProvider.Setup(m => m.GetFlight(It.IsAny<int>())).Returns(FlightTestData.Flight(3));

            var fc = GetTestSubject();

            var result = fc.Get(3) as OkNegotiatedContentResult<Flight>;

            result.Content.ShouldBeEquivalentTo(FlightTestData.Flight(3));
        }

        [Test]
        public void ShouldReturnBadRequestOnNullInput()
        {
            var fc = GetTestSubject();

            var result = fc.Post((Flight)null) as BadRequestResult;

            result.Should().NotBeNull();
        }

        [Test]
        public void ShouldReturnCreatedResult()
        {
            var fc = GetTestSubject();

            var result = fc.Post(FlightTestData.Flight(1)) as CreatedNegotiatedContentResult<Flight>;

            result.Should().NotBeNull();
        }

        [Test]
        public void ShouldReturnCreatedFlight()
        {
            var fc = GetTestSubject();

            var result = fc.Post(FlightTestData.Flight(1)) as CreatedNegotiatedContentResult<Flight>;

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