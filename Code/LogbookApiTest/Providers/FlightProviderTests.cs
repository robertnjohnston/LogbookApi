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

        [SetUp]
        public void SetupTests()
        {
            Context = new Mock<jetstrea_LogbookEntities>();
            FlightDbSet = new Mock<DbSet<Flight>>();
        }

        [Test]
        public void ShouldReturnProvider()
        {
            var fp = GetTestSubject();
            fp.Should().BeAssignableTo<IFlightProvider>();
        }

        [Test]
        public void ShouldThrowArgumentNullexceptionOnnullContext()
        {
            Action act = () => new FlightProvider(null);

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("context");
        }

        [Test]
        public void ShouldReturnNullOnNothingFound()
        {
            FlightDbSet.Setup(m => m.Find(1)).Returns((Flight) null);
            Context.Setup(m => m.Flight).Returns(FlightDbSet.Object);
            var fp = GetTestSubject();

            var result = fp.GetFlight(99);

            result.Should().Be(null);
        }

        [Test]
        public void ShouldReturnFlight()
        {
            FlightDbSet.Setup(m => m.Find(1)).Returns(FlightTestData.Flight(1));
            Context.Setup(m => m.Flight).Returns(FlightDbSet.Object);
            var fp = GetTestSubject();

            var result = fp.GetFlight(1);

            result.Should().Be(FlightTestData.Flight(1));
        }

        private IFlightProvider GetTestSubject()
        {
            return new FlightProvider(Context.Object);
        }
    }
}
