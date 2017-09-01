using System;
using System.Data.Entity;
using System.Linq;
using FluentAssertions;
using LogbookApi;
using LogbookApi.Providers;
using LogbookApi.Providers.Implementation;
using Moq;
using NUnit.Framework;

namespace LogbookApiTest.Providers
{
    [TestFixture()]
    public class AircraftProviderTests
    {
        private Mock<jetstrea_LogbookEntities> Context { get; set; }
        
        private Mock<IEntityProvider<Aircraft>> MockAircraftProvider { get; set; }
       
        private Mock<DbSet<Aircraft>> AircraftDbSet { get; set; }

        [SetUp]
        public void SetupTests()
        {
            Context = new Mock<jetstrea_LogbookEntities>();

            MockAircraftProvider = new Mock<IEntityProvider<Aircraft>>();
            AircraftDbSet = new Mock<DbSet<Aircraft>>();
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
            AircraftDbSet.Setup(m => m.Find(1)).Returns((Aircraft)null);
            Context.Setup(m => m.Aircraft).Returns(AircraftDbSet.Object);

            var result = GetTestSubject().Get(1);

            result.Should().Be(null);
        }

        [Test]
        public void ShouldReturnNewAircraftOnNothingFoundName()
        {
            //AircraftDbSet.Setup(m => m.First(aircraft => aircraft.Name=="F16")).Returns((Aircraft)null);
            //AircraftDbSet.Setup(m => m.Add(It.IsAny<Aircraft>())).Returns(new Aircraft {Id = 1, Name = "F16"});
            Context.Setup(m => m.Aircraft.FirstOrDefault(f => f.Name == "F16")).Returns((Aircraft)null);
            MockAircraftProvider.Setup(m => m.Save(It.IsAny<Aircraft>())).Returns(new Aircraft {Id = 1, Name = "F16"});

            var result = GetTestSubject().Get("F16");

            result.Should().BeAssignableTo<Aircraft>();
        }

        private IEntityProvider<Aircraft> GetTestSubject()
        {
            return new AircraftProvider(Context.Object);
        }
    }
}
