using FluentAssertions;
using LogbookApi;
using LogbookApi.Providers;
using LogbookApi.Providers.Implementation;
using Moq;
using NUnit.Framework;
using System;
using System.Data.Entity;
using System.Linq;
using LogbookApiTest.TestData;
using LogbookApiTest.TestData.Implementation;

namespace LogbookApiTest.Providers
{
    [TestFixture()]
    public class AircraftProviderTests
    {
        private Mock<jetstrea_LogbookEntities> Context { get; set;  }
        private Mock<DbSet<Aircraft>> AircraftDbSet { get; set; }


        [SetUp]
        public void SetupTests()
        {
            Context = new Mock<jetstrea_LogbookEntities>();
            AircraftDbSet = new Mock<DbSet<Aircraft>>();
            SetupDbSet();
        }

        [Test]
        public void ShouldReturnProvider()
        {
            var fp = GetTestSubject();
            fp.Should().BeAssignableTo<IEntityProvider<Aircraft>>();
        }

        [Test]
        public void ShouldThrowArgumentNullexceptionOnNullContext()
        {
            Action act = () => new AircraftProvider(null);

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("context");
        }

        [Test]
        public void ShouldReturnNullOnNothingFoundId()
        {
            var result = GetTestSubject().Get(1);

            result.Should().Be(null);
        }

        [Test]
        public void ShouldReturnAnAircraftOnValidId()
        {
            AircraftDbSet.Setup(m => m.Find(1)).Returns(FlightTestData.Aircraft());

            var result = GetTestSubject().Get(1);

            result.ShouldBeEquivalentTo(FlightTestData.Aircraft());
        }

        [Test]
        public void ShouldReturnNewAircraftOnSave()
        {
            var newAircraft = new Aircraft {Name = "Puchasz" };
            AircraftDbSet.Setup(m => m.Add(newAircraft)).Returns(newAircraft);
            Context.Setup(m => m.SaveChanges());

            var result = GetTestSubject().Save(newAircraft);
            Context.Verify();
        }

        [Test]
        public void ShouldReturnAnotherNewAircraftOnSave()
        {
            var newAircraft = new Aircraft { Name = "Cirrus" };
            AircraftDbSet.Setup(m => m.Add(newAircraft)).Returns(newAircraft);
            Context.Setup(m => m.SaveChanges());

            var result = GetTestSubject().Save(newAircraft);
            result.Id.Should().Be(4);
        }

        [Test]
        public void ShouldReturnExistingAircraftOnSave()
        {
            var aircraft = new Aircraft { Id = 7, Name = "Puchasz" };
 
            var result = GetTestSubject().Save(aircraft);
            result.ShouldBeEquivalentTo(aircraft);
        }

        private IEntityProvider<Aircraft> GetTestSubject()
        {
            return new AircraftProvider(Context.Object);
        }

        private void SetupDbSet()
        {
            AircraftDbSet.As<IQueryable<Aircraft>>().Setup(m => m.Provider)
                       .Returns(FlightTestData.AircraftData.AsQueryable().Provider);
            AircraftDbSet.As<IQueryable<Flight>>().Setup(m => m.Expression)
                       .Returns(FlightTestData.FlightData.AsQueryable().Expression);
            AircraftDbSet.As<IQueryable<Flight>>().Setup(m => m.ElementType)
                       .Returns(FlightTestData.FlightData.AsQueryable().ElementType);
            AircraftDbSet.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator())
                       .Returns(FlightTestData.FlightData.AsQueryable().GetEnumerator());

            AircraftDbSet.SetupGet(p => p.Local).Returns(FlightTestData.Aircrafts);
            Context.Setup(m => m.Aircraft).Returns(AircraftDbSet.Object);
        }
    }
}
