using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using FluentAssertions;
using LogbookApi.Controllers;
using LogbookApi.Models;
using LogbookApi.Providers;
using LogbookApiTest.TestData;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LogbookApiTest
{
    [TestFixture]
    public class FlightControllerTest
    {
        private Mock<IFlight> MockFlightProvider { get; set; }

        private Mock<IPage> MockPageProvider { get; set; }

        [SetUp]
        public void SetupTests()
        {
            MockFlightProvider = new Mock<IFlight>();
            MockPageProvider = new Mock<IPage>();
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionOnMissingFlightProvider()
        {
            Action act = () => new FlightController(null);

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("flightProvider");
        }

        //[Test]
        //public void ShouldThrowArgumentNullExceptionOnMissingPageProvider()
        //{
        //    Action act = () => new FlightController(MockFlightProvider.Object, null);

        //    act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("pageProvider");
        //}

        [Test]
        public void ShouldReturnAFlightController()
        {
            var fc = GetTestSubject();
            fc.Should().BeAssignableTo<FlightController>();
        }

        [Test]
        public void ShouldReturnNotFoundOnInvalidPageNumber()
        {
            MockFlightProvider.Setup(m => m.GetFlightsByPage(It.IsAny<int>())).Returns(Task.FromResult((List<Flight>)null));

            var fc = GetTestSubject();

            var result =  fc.Get(0).Result as NotFoundResult;

            result.Should().NotBe(null);
        }

        [Test]
        public void ShouldReturnNotFoundOnPageNumberNotFound()
        {
            MockFlightProvider.Setup(m => m.GetFlightsByPage(It.IsAny<int>())).Returns(Task.FromResult((List<Flight>)null));

            var fc = GetTestSubject();

            var result = fc.Get(2).Result as NotFoundResult;

            result.Should().NotBe(null);
        }

        [Test]
        public void ShouldReturnOkResult()
        {
            MockFlightProvider.Setup(m => m.GetFlightsByPage(1)).Returns(Task.FromResult(FlightsByPage.Page()));

            var fc = GetTestSubject();

            var result = fc.Get(1).Result as OkNegotiatedContentResult<List<Flight>>;

            result.Should().NotBe(null);
        }

        [Test]
        public void ShouldReturnListofFlights()
        {
            MockFlightProvider.Setup(m => m.GetFlightsByPage(1)).Returns(Task.FromResult(FlightsByPage.Page()));

            var fc = GetTestSubject();

            var result = fc.Get(1).Result as OkNegotiatedContentResult<List<Flight>>;

            GetContent(result).Count.Should().Be(10);
        }

        [Test]
        public void ShouldReturnNotFoundOnFilter()
        {
            MockFlightProvider.Setup(m => m.GetFlightsByPage(1)).Returns(Task.FromResult((List<Flight>)null));

            var fc = GetTestSubject();

            var result = fc.Get(new FlightFilter()).Result as NotFoundResult;

            result.Should().NotBe(null);
        }

        [Test]
        public void ShouldReturnListOfFlightsOnFilter()
        {
            MockFlightProvider.Setup(m => m.GetFilteredFlights(It.IsAny<FlightFilter>())).Returns(Task.FromResult(FlightsByPage.FilteredFlights()));

            var fc = GetTestSubject();

            var result = fc.Get(new FlightFilter()).Result as OkNegotiatedContentResult<List<Flight>>;

            GetContent(result).Count.Should().Be(3);
        }

        [Test]
        public void ShouldReturnOneFlight()
        {
            MockFlightProvider.Setup(m => m.GetFilteredFlights(It.IsAny<FlightFilter>())).Returns(Task.FromResult(FlightsByPage.Flight(new FlightFilter())));

            var fc = GetTestSubject();

            var result = fc.Get(new FlightFilter()).Result as OkNegotiatedContentResult<List<Flight>>;

            GetContent(result).Count.Should().Be(1);
        }

        [Test]
        public void ShouldGetBadRequestOnInvalidParameterType()
        {
            var fc = GetTestSubject();

            var result = fc.Get("Hello").Result as BadRequestErrorMessageResult;

            result.Should().NotBe(null);
        }

        private FlightController GetTestSubject()
        {
             return new FlightController(MockFlightProvider.Object)
             {
                 ControllerContext = new HttpControllerContext
                 { Configuration = new HttpConfiguration() }, Request = new HttpRequestMessage()
             };
        }

        private static T GetContent<T>(OkNegotiatedContentResult<T> result)
        {
            var content = result?.ExecuteAsync(new CancellationToken()).Result;
            return JsonConvert.DeserializeObject<T>(content?.Content.ReadAsStringAsync().Result);
        }
    }
}