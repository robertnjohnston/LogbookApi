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
using LogbookApiTest.TestData;

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
            FlightDbSet.SetupGet(p => p.Local).Returns(FlightTestData.Flights());
            Context.Setup(m => m.Flight).Returns(FlightDbSet.Object);

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
                    new FlightFilter { FilterType = FilterType.Date, DateStart = new DateTime(1900, 1,1), DateEnd = new DateTime(2020, 12,31) });

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
                .Returns(FlightTestData.Data.AsQueryable().Provider);
            FlightDbSet.As<IQueryable<Flight>>().Setup(m => m.Expression)
                .Returns(FlightTestData.Data.AsQueryable().Expression);
            FlightDbSet.As<IQueryable<Flight>>().Setup(m => m.ElementType)
                .Returns(FlightTestData.Data.AsQueryable().ElementType);
            FlightDbSet.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator())
                .Returns(FlightTestData.Data.AsQueryable().GetEnumerator());

            FlightDbSet.SetupGet(p => p.Local).Returns(FlightTestData.Flights());
            Context.Setup(m => m.Flight).Returns(FlightDbSet.Object);

        }

        private IFlightProvider GetTestSubject()
        {
            return new FlightProvider(Context.Object, MockAirfieldProvider.Object, MockAircraftProvider.Object);
        }
    }
}
