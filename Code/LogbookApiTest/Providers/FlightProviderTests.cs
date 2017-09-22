using System;
using System.Data.Entity;
using System.Linq;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using LogbookApi;
using LogbookApi.Exceptions;
using LogbookApi.Models;
using LogbookApi.Providers;
using LogbookApi.Providers.Implementation;
using LogbookApiTest.TestData.Implementation;

namespace LogbookApiTest.Providers
{
    [TestFixture]
    public class FlightProviderTests
    {
        private Mock<jetstrea_LogbookEntities> Context { get; set; }
       
        private Mock<DbSet<Flight>> FlightDbSet { get; set; }

        private Mock<IEntityProvider<Airfield>> MockAirfieldProvider { get; set; }

        private Mock<IEntityProvider<Aircraft>> MockAircraftProvider { get; set; }

        [SetUp]
        public void SetupTests()
        {
            Context = new Mock<jetstrea_LogbookEntities>();
            FlightDbSet = new Mock<DbSet<Flight>>();

            MockAirfieldProvider = new Mock<IEntityProvider<Airfield>>();

            MockAircraftProvider = new Mock<IEntityProvider<Aircraft>>();

            SetupDbSet();
        }

        [Test]
        public void ShouldReturnProvider()
        {
            var fp = GetTestSubject();
            fp.Should().BeAssignableTo<IFlightProvider>();
        }

        [Test]
        public void ShouldThrowArgumentNullexceptionOnNullContext()
        {
            Action act = () => new FlightProvider(null, null, null);

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("context");
        }

        [Test]
        public void ShouldThrowArgumentNullexceptionOnNullAirfieldProvider()
        {
            Action act = () => new FlightProvider(Context.Object, null, null);

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("airfieldProvider");
        }

        [Test]
        public void ShouldThrowArgumentNullexceptionOnNullAircraftProvider()
        {
            Action act = () => new FlightProvider(Context.Object, MockAirfieldProvider.Object, null);

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("aircraftProvider");
        }

        [Test]
        public void ShouldReturnNullOnNothingFound()
        {
            var result = GetTestSubject().GetFlight(99);

            result.Should().Be(null);
        }

        [Test]
        public void ShouldReturnFlight()
        {
            var resultData = SetupTest();

            var result = GetTestSubject().GetFlight(1);

            result.ShouldBeEquivalentTo(resultData);
        }

        [Test]
        public void ShouldThrowInvalidFilterException()
        {
            var fp = GetTestSubject();

            Action act = () => fp.GetFilteredFlights(new FlightFilter());

            act.ShouldThrow<InvalidFilterException>();
        }

        [Test]
        public void ShouldReturnAnEmptyListOnNothingFoundNumber()
        {
            SetupDbSet();
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(
                    new FlightFilter {FilterType = FilterType.Number, FlightStart = 9999, FlightEnd = 10000});

            result.Count.Should().Be(0);
        }

        [Test]
        public void ShouldReturnAListOfFlightsNumber()
        {
            SetupDbSet();

            var fp = GetTestSubject();
            
            var result =
                fp.GetFilteredFlights(
                    new FlightFilter { FilterType = FilterType.Number, FlightStart = 1, FlightEnd = 10 });

            result.Count.Should().Be(10);
        }

        [Test]
        public void ShouldReturnAnEmptyListOnNothingFoundDate()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(
                    new FlightFilter { FilterType = FilterType.Date, DateStart = DateTime.Now.AddDays(30), DateEnd = DateTime.Now.AddDays(31) });

            result.Count.Should().Be(0);
        }

        [Test]
        public void ShouldReturnAListOfFlightsDate()
        {
            Context.Setup(m => m.Flight).Returns(FlightDbSet.Object);

            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(
                    new FlightFilter { FilterType = FilterType.Date, DateStart = new DateTime(1900, 1,1), DateEnd = new DateTime(1988, 12,31) });

            result.Count.Should().Be(10);
        }

        [Test]
        public void ShouldReturnAnEmptyListOnNothingFoundAircraft()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(
                    new FlightFilter { FilterType = FilterType.Aircraft, Aircraft = 99 });

            result.Count.Should().Be(0);
        }

        [Test]
        public void ShouldReturnAListOfFlightsAircraft()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(
                    new FlightFilter { FilterType = FilterType.Aircraft, Aircraft = 1 });

            result.Count.Should().Be(10);
        }

        [Test]
        public void ShouldReturnAnEmptyListOnNothingFoundAirfield()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(new FlightFilter { FilterType = FilterType.Airfield, Airfield = 99 });

            result.Count.Should().Be(0);
        }

        [Test]
        public void ShouldReturnAListOfFlightsAirfield()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(new FlightFilter { FilterType = FilterType.Airfield, Airfield = 1 });

            result.Count.Should().Be(10);
        }

        [Test]
        public void ShouldReturnAnEmptyListOnNothingFoundLaunch()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(new FlightFilter { FilterType = FilterType.Launch, Launch = "X" });

            result.Count.Should().Be(0);
        }

        [Test]
        public void ShouldReturnAListOfFlightsLaunch()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(new FlightFilter { FilterType = FilterType.Launch, Launch = "A" });

            result.Count.Should().Be(20);
        }

        [Test]
        public void ShouldReturnAnEmptyListOnNothingFoundCrew()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(new FlightFilter { FilterType = FilterType.Crew, Crew = 0 });

            result.Count.Should().Be(0);
        }

        [Test]
        public void ShouldReturnAListOfFlightsCrew()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(new FlightFilter { FilterType = FilterType.Crew, Crew = 1 });

            result.Count.Should().Be(10);
        }

        [Test]
        public void ShouldReturnEmptyListOnFilterNumberandAircraft()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(new FlightFilter { FilterType = FilterType.Number | FilterType.Aircraft, FlightStart = 1, FlightEnd = 10, Aircraft = 2});

            result.Count.Should().Be(0);
        }

        [Test]
        public void ShouldReturnListOfFlightsOnFilterNumberandAircraft()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(new FlightFilter { FilterType = FilterType.Number | FilterType.Aircraft, FlightStart = 1, FlightEnd = 20, Aircraft = 2 });

            result.Count.Should().NotBe(0);
        }

        [Test]
        public void ShouldReturnEmptyListOnFilterDateandAircraft()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(new FlightFilter { FilterType = FilterType.Date | FilterType.Aircraft, DateStart = new DateTime(1988, 01, 01), DateEnd = new DateTime(1988, 04, 30), Aircraft = 2 });

            result.Count.Should().Be(0);
        }


        [Test]
        public void ShouldReturnListOfFlightsOnFilterDateandAircraft()
        {
            var fp = GetTestSubject();

            var result =
                fp.GetFilteredFlights(new FlightFilter { FilterType = FilterType.Date | FilterType.Aircraft, DateStart = new DateTime(1988, 01, 01) , DateEnd = DateTime.Now, Aircraft = 2 });

            result.Count.Should().NotBe(0);
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionOnNullFlight()
        {
            var fp = GetTestSubject();

            Action act = () => fp.SaveFlight(null);

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("flight");
        }

        [Test]
        public void ShouldThrowInvalidFlightExceptionOnSave()
        {
            var fp = GetTestSubject();

            Action act = () => fp.SaveFlight(new Flight());

            act.ShouldThrow<InvalidFlightException>().And.Message.Length.Should().BeGreaterThan(0);
        }

        [Test]
        public void ShouldSaveFlight()
        {
            var fp = GetTestSubject();

            Action act = () => fp.SaveFlight(new Flight());
        }

        private Flight SetupTest()
        {
            var testFlight = FlightTestData.Flight(1);
            FlightDbSet.Setup(m => m.Find(1)).Returns(testFlight);
            Context.Setup(m => m.Flight).Returns(FlightDbSet.Object);
            MockAirfieldProvider.Setup(m => m.Get(1)).Returns(FlightTestData.Airfield);
            MockAircraftProvider.Setup(m => m.Get(1)).Returns(FlightTestData.Aircraft);
            testFlight.Airfield = FlightTestData.Airfield().Name;
            testFlight.Aircraft = FlightTestData.Aircraft().Name;
            return testFlight;
        }

        private void SetupDbSet()
        {
            FlightDbSet.As<IQueryable<Flight>>().Setup(m => m.Provider)
                .Returns(FlightTestData.FlightData.AsQueryable().Provider);
            FlightDbSet.As<IQueryable<Flight>>().Setup(m => m.Expression)
                .Returns(FlightTestData.FlightData.AsQueryable().Expression);
            FlightDbSet.As<IQueryable<Flight>>().Setup(m => m.ElementType)
                .Returns(FlightTestData.FlightData.AsQueryable().ElementType);
            FlightDbSet.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator())
                .Returns(FlightTestData.FlightData.AsQueryable().GetEnumerator());

            FlightDbSet.SetupGet(p => p.Local).Returns(FlightTestData.Flights());
            Context.Setup(m => m.Flight).Returns(FlightDbSet.Object);
        }

        private IFlightProvider GetTestSubject()
        {
            return new FlightProvider(Context.Object, MockAirfieldProvider.Object, MockAircraftProvider.Object);
        }
    }
}
