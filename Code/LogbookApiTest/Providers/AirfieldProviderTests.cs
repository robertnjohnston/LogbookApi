using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using FluentAssertions;
using LogbookApi;
using LogbookApi.Providers;
using LogbookApi.Providers.Implementation;
using LogbookApiTest.TestData;
using Moq;
using NUnit.Framework;


namespace LogbookApiTest.Providers
{
    [TestFixture()]
    public class AirfieldProviderTests
    {
        private Mock<jetstrea_LogbookEntities> Context { get; set; }
        private Mock<DbSet<Airfield>> Airfield { get; set; }


        [SetUp]
        public void SetupTests()
        {
            Context = new Mock<jetstrea_LogbookEntities>();
            Airfield = new Mock<DbSet<Airfield>>();
            Context.Setup(m => m.Airfield).Returns(Airfield.Object);
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

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("context");
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
            Airfield.Setup(m => m.Find(1)).Returns(FlightTestData.Airfield());

            var result = GetTestSubject().Get(1);

            result.ShouldBeEquivalentTo(FlightTestData.Airfield());
        }

        [Test]
        public void ShouldReturnNewAirfieldOnSave()
        {
            var newAirfield = new Airfield { Name = "NewAirfield" };
            Airfield.Setup(m => m.Add(newAirfield)).Returns(newAirfield);
            Airfield.SetupGet(p => p.Local).Returns(new ObservableCollection<Airfield>());
            Context.Setup(m => m.Airfield).Returns(Airfield.Object);
            Context.Setup(m => m.SaveChanges());

            var result = GetTestSubject().Save(newAirfield);
            Context.Verify();
        }

        [Test]
        public void ShouldReturnAnotherNewAirfieldOnSave()
        {
            var newAirfield = new Airfield { Name = "AnotherNewAirfield" };
            Airfield.Setup(m => m.Add(newAirfield)).Returns(newAirfield);
            Airfield.SetupGet(p => p.Local).Returns(new ObservableCollection<Airfield> { new Airfield { Id = 1, Name = "NewAirfield" } });
            Context.Setup(m => m.Airfield).Returns(Airfield.Object);
            Context.Setup(m => m.SaveChanges());

            var result = GetTestSubject().Save(newAirfield);
            result.Id.Should().Be(2);
        }

        [Test]
        public void ShouldReturnExistingAirfieldOnSave()
        {
            var Airfield = new Airfield { Id = 7, Name = "Airfield" };

            var result = GetTestSubject().Save(Airfield);
            result.ShouldBeEquivalentTo(Airfield);
        }

        private IEntityProvider<Airfield> GetTestSubject()
        {
            return new AirfieldProvider(Context.Object);
        }

    }
}
