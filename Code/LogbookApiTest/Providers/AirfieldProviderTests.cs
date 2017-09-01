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
    public class AirfieldProviderTests
    {
        private Mock<jetstrea_LogbookEntities> Context { get; set; }

        private Mock<IEntityProvider<Airfield>> MockAirfieldProvider { get; set; }
        private Mock<DbSet<Airfield>> AirfieldDbSet { get; set; }
        [SetUp]
        public void SetupTests()
        {
            Context = new Mock<jetstrea_LogbookEntities>();

            MockAirfieldProvider = new Mock<IEntityProvider<Airfield>>();
            AirfieldDbSet = new Mock<DbSet<Airfield>>();
        }

        [Test]
        public void ShouldThrowArgumentNullexceptionOnNullContext()
        {
            Action act = () => new AirfieldProvider(null);

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("context");
        }

        private IEntityProvider<Airfield> GetTestSubject()
        {
            return new AirfieldProvider(Context.Object);
        }
    }
}
