using System;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using LogbookApi.Database;
using LogbookApi.Providers;
using LogbookApi.Providers.Implementation;
using LogbookApiTest.TestData.Implementation;
using Moq;
using NUnit.Framework;


namespace LogbookApiTest.Providers
{
    [ExcludeFromCodeCoverage]
    [TestFixture()]
    public class AirfieldProviderTests
    {
        private Mock<jetstrea_LogbookEntities> Context { get; set; }
        private Mock<DbSet<Airfield>> AirfieldDbSet { get; set; }


        [SetUp]
        public void SetupTests()
        {
            Context = new Mock<jetstrea_LogbookEntities>();
            AirfieldDbSet = new Mock<DbSet<Airfield>>();
            SetupDbSet();
        }

        [Test]
        public void ShouldReturnProvider()
        {
            var fp = GetTestSubject();
            fp.Should().BeAssignableTo<IEntityProvider<Airfield>>();
        }

        [Test]
        public void ShouldThrowArgumentNullexceptionOnNullContext()
        {
            Action act = () => new AirfieldProvider(null);

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("context");
        }

        [Test]
        public void ShouldReturnNullOnNothingFoundId()
        {
            var result = GetTestSubject().Get(1);

            result.Should().Be(null);
        }

        [Test]
        public void ShouldReturnAnAirfieldOnValidId()
        {
            AirfieldDbSet.Setup(m => m.Find(1)).Returns(FlightTestData.Airfield());

            var result = GetTestSubject().Get(1);

            result.Should().BeEquivalentTo(FlightTestData.Airfield());
        }

        [Test]
        public void ShouldReturnNewAirfieldOnSave()
        {
            var newAirfield = new Airfield { Name = "NewAirfield" };
            AirfieldDbSet.Setup(m => m.Add(newAirfield)).Returns(newAirfield);
            Context.Setup(m => m.SaveChanges());

            var result = GetTestSubject().Save(newAirfield);
            Context.Verify();
        }

        [Test]
        public void ShouldReturnAnotherNewAirfieldOnSave()
        {
            var newAirfield = new Airfield { Name = "AnotherNewAirfield" };
            AirfieldDbSet.Setup(m => m.Add(newAirfield)).Returns(newAirfield);
            Context.Setup(m => m.SaveChanges());

            var result = GetTestSubject().Save(newAirfield);
            result.Id.Should().Be(4);
        }

        [Test]
        public void ShouldReturnExistingAirfieldOnSave()
        {
            var Airfield = new Airfield { Id = 7, Name = "Airfield" };

            var result = GetTestSubject().Save(Airfield);
            result.Should().BeEquivalentTo(Airfield);
        }

        private IEntityProvider<Airfield> GetTestSubject()
        {
            return new AirfieldProvider(Context.Object);
        }
        private void SetupDbSet()
        {
            AirfieldDbSet.As<IQueryable<Airfield>>().Setup(m => m.Provider)
                         .Returns(FlightTestData.AirfieldData.AsQueryable().Provider);
            AirfieldDbSet.As<IQueryable<Airfield>>().Setup(m => m.Expression)
                         .Returns(FlightTestData.AirfieldData.AsQueryable().Expression);
            AirfieldDbSet.As<IQueryable<Airfield>>().Setup(m => m.ElementType)
                         .Returns(FlightTestData.AirfieldData.AsQueryable().ElementType);
            AirfieldDbSet.As<IQueryable<Airfield>>().Setup(m => m.GetEnumerator())
                         .Returns(FlightTestData.AirfieldData.AsQueryable().GetEnumerator());

            AirfieldDbSet.SetupGet(p => p.Local).Returns(FlightTestData.Airfields);
            Context.Setup(m => m.Airfield).Returns(AirfieldDbSet.Object);
        }
    }
}
