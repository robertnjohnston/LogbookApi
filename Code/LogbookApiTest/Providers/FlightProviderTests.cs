using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using LogbookApi;
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
            FlightDbSet.Setup(m => m.Find(1)).Returns((Flight) null);
            Context.Setup(m => m.Flight).Returns(FlightDbSet.Object);

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

        private IFlightProvider GetTestSubject()
        {
            return new FlightProvider(Context.Object, MockAirfieldProvider.Object, MockAircraftProvider.Object);
        }
    }
}
