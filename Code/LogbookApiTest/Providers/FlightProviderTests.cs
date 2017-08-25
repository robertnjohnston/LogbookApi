using System;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using LogbookApi;
using LogbookApi.Providers;
using LogbookApi.Providers.Implementation;

namespace LogbookApiTest.Providers
{
    [TestFixture]
    public class FlightProviderTests
    {
        private Mock<jetstrea_LogbookEntities> Context { get; set; }

        [SetUp]
        public void SetupTests()
        {
            Context = new Mock<jetstrea_LogbookEntities>();
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

        //[Test]
        //public void ShouldReturnEmptyListOnNothingFound()
        //{
        //    Context.Setup(m => m.GetFlightsForPage(It.IsAny<int>())).Returns((ObjectResult<Flight>)null);
        //    var fp = GetTestSubject();

        //    var result = fp.GetFlightsByPage(1);

        //    result.Count.Should().Be(0);
        //}

        private IFlightProvider GetTestSubject()
        {
            return new FlightProvider(Context.Object);
        }
    }
}
