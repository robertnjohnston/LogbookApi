using FluentAssertions;
using LogbookApi;
using LogbookApi.Providers;
using LogbookApi.Providers.Implementation;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using LogbookApiTest.TestData;

namespace LogbookApiTest.Providers
{
    [TestFixture()]
    public class AircraftProviderTests
    {
        private Mock<jetstrea_LogbookEntities> Context { get; set;  }
        private Mock<DbSet<Aircraft>> Aircraft { get; set; }


        [SetUp]
        public void SetupTests()
        {
            Context = new Mock<jetstrea_LogbookEntities>();
            Aircraft = new Mock<DbSet<Aircraft>>();
            Context.Setup(m => m.Aircraft).Returns(Aircraft.Object);
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
            Aircraft.Setup(m => m.Find(1)).Returns(FlightTestData.Aircraft());

            var result = GetTestSubject().Get(1);

            result.ShouldBeEquivalentTo(FlightTestData.Aircraft());
        }

        [Test]
        public void ShouldReturnNewAircraftOnSave()
        {
            var newAircraft = new Aircraft {Name = "NewAircraft"};
            Aircraft.Setup(m => m.Add(newAircraft)).Returns(newAircraft);
            Aircraft.SetupGet(p => p.Local).Returns(new ObservableCollection<Aircraft>());
            Context.Setup(m => m.Aircraft).Returns(Aircraft.Object);
            Context.Setup(m => m.SaveChanges());

            var result = GetTestSubject().Save(newAircraft);
            Context.Verify();
        }

        [Test]
        public void ShouldReturnAnotherNewAircraftOnSave()
        {
            var newAircraft = new Aircraft { Name = "AnotherNewAircraft" };
            Aircraft.Setup(m => m.Add(newAircraft)).Returns(newAircraft);
            Aircraft.SetupGet(p => p.Local).Returns(new ObservableCollection<Aircraft> {new Aircraft {Id=1,Name= "NewAircraft" } });
            Context.Setup(m => m.Aircraft).Returns(Aircraft.Object);
            Context.Setup(m => m.SaveChanges());

            var result = GetTestSubject().Save(newAircraft);
            result.Id.Should().Be(2);
        }

        [Test]
        public void ShouldReturnExistingAircraftOnSave()
        {
            var aircraft = new Aircraft { Id = 7, Name = "Aircraft" };
 
            var result = GetTestSubject().Save(aircraft);
            result.ShouldBeEquivalentTo(aircraft);
        }

        private IEntityProvider<Aircraft> GetTestSubject()
        {
            return new AircraftProvider(Context.Object);
        }
    }
}
