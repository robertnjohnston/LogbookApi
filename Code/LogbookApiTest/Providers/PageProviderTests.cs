using System;
using System.Data.Entity;
using System.Linq;
using FluentAssertions;
using LogbookApi;
using LogbookApi.Exceptions;
using LogbookApi.Providers;
using LogbookApi.Providers.Implementation;
using LogbookApiTest.TestData.Implementation;
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
        
        [SetUp]
        public void SetupTests()
        {
            Context = new Mock<jetstrea_LogbookEntities>();
            FlightDbSet = new Mock<DbSet<Flight>>();
            PageDbSet = new Mock<DbSet<Page>>();
            SetupDbSet();
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

        public void ShouldReturnNullOnPageNotFound()
        {
            var result = GetTestSubject().GetPage(99);

            result.Should().BeNull();
        }

        [Test]
        public void ShouldReturnPageOnPageFound()
        {
            var resultData = SetupTest();

            var result = GetTestSubject().GetPage(1);

            result.Should().Be(resultData);
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionOnSaveWithNullPage()
        {
            var pp = GetTestSubject();
            
            Action act = () => pp.SavePage(null);

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("page");
        }

        [Test]
        public void ShouldThrowInvalidPageExceptionOnSaveWithInvalidpage()
        {
            var pp = GetTestSubject();

            Action act = () => pp.SavePage(new Page());

            act.ShouldThrow<InvalidPageException>();
        }

        private IPageProvider GetTestSubject()
        {
            return new PageProvider(Context.Object);
        }

        private void SetupDbSet()
        {
            PageDbSet.As<IQueryable<Page>>().Setup(m => m.Provider)
                       .Returns(FlightTestData.PageData.AsQueryable().Provider);
            PageDbSet.As<IQueryable<Page>>().Setup(m => m.Expression)
                       .Returns(FlightTestData.PageData.AsQueryable().Expression);
            PageDbSet.As<IQueryable<Page>>().Setup(m => m.ElementType)
                       .Returns(FlightTestData.PageData.AsQueryable().ElementType);
            PageDbSet.As<IQueryable<Page>>().Setup(m => m.GetEnumerator())
                       .Returns(FlightTestData.PageData.AsQueryable().GetEnumerator());

            PageDbSet.SetupGet(p => p.Local).Returns(FlightTestData.Pages);
            Context.Setup(m => m.Page).Returns(PageDbSet.Object);
        }

        private Page SetupTest()
        {
            var testPage = FlightTestData.Page();
            PageDbSet.Setup(m => m.Find(1)).Returns(testPage);
            Context.Setup(m => m.Page).Returns(PageDbSet.Object);
            return testPage;
        }
    }
}
