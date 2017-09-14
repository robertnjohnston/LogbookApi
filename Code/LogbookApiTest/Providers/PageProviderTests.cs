using System;
using System.Data.Entity;
using FluentAssertions;
using LogbookApi;
using LogbookApi.Providers;
using LogbookApi.Providers.Implementation;
using Moq;
using NUnit.Framework;

namespace LogbookApiTest.Providers
{
    [TestFixture()]
    public class PageProviderTests
    {
        private Mock<jetstrea_LogbookEntities> Context { get; set; }

        private Mock<DbSet<Flight>> FlightDbSet { get; set; }

        private Mock<DbSet<Page>> PageDbSet { get; set; }

        //private Mock<IEntityProvider<Airfield>> MockAirfieldProvider { get; set; }

        //private Mock<IEntityProvider<Aircraft>> MockAircraftProvider { get; set; }

        [SetUp]
        public void SetupTests()
        {
            Context = new Mock<jetstrea_LogbookEntities>();
            FlightDbSet = new Mock<DbSet<Flight>>();
            PageDbSet = new Mock<DbSet<Page>>();

            //MockAirfieldProvider = new Mock<IEntityProvider<Airfield>>();

            //MockAircraftProvider = new Mock<IEntityProvider<Aircraft>>();
        }
        [Test]
        public void ShouldReturnProvider()
        {
            var pp = GetTestSubject();
            pp.Should().BeAssignableTo<IPageProvider>();
        }

        [Test]
        public void ShouldThrowArgumentNullexceptionOnNullContext()
        {
            Action act = () => new PageProvider(null);

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("context");
        }

        [Test]

        public void ShouldReturnPageOnPageFound()
        {
            var pp = GetTestSubject();
        }

        private IPageProvider GetTestSubject()
        {
            return new PageProvider(Context.Object);
        }
    }
}
