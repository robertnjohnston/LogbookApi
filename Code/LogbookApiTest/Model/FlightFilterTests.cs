using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using LogbookApi.Models;
using NUnit.Framework;

namespace LogbookApiTest.Model
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class FlightFilterTests
    {
    #region General
        [Test]
        public void ShouldReturnAFilter()
        {
            var filter = new FlightFilter();
            filter.Should().BeAssignableTo(typeof(FlightFilter));
        }

        [Test]
        public void ShouldReturnAnInvalidFilter()
        {
            var filter = new FlightFilter();
            filter.IsValid().Should().BeFalse();
        }

        [Test]
        public void ShouldReturnAnInvalidFilterOnNumberAndDateCombined()
        {
            var filter = new FlightFilter {FilterType = FilterType.Date | FilterType.Number};
            filter.IsValid().Should().BeFalse();
        }
     #endregion

    #region Dates
        [Test]
        public void ShouldReturnAnInvalidFilterOnInvalidDateStartMissing()
        {
            var filter = new FlightFilter {FilterType = FilterType.Date, DateEnd = DateTime.Now};
            filter.IsValid().Should().BeFalse();
        }

        [Test]
        public void ShouldReturnAnInvalidFilterOnInvalidDateEndMissing()
        {
            var filter = new FlightFilter { FilterType = FilterType.Date, DateStart = DateTime.Now};
            filter.IsValid().Should().BeFalse();
        }

        [Test]
        public void ShouldReturnAnInvalidFilterOnDateStartGreaterThanDateEnd()
        {
            var filter = new FlightFilter { FilterType = FilterType.Date, DateStart = DateTime.Now.AddDays(1), DateEnd = DateTime.Now };
            filter.IsValid().Should().BeFalse();
        }

        [Test]
        public void ShouldReturnAValidFilterOnDateStartEqualDateEnd()
        {
            var date = DateTime.Now;
            var filter = new FlightFilter { FilterType = FilterType.Date, DateStart = date, DateEnd = date };
            filter.IsValid().Should().BeTrue();
        }

        [Test]
        public void ShouldReturnAValidFilterOnDateStartLessThanDateEnd()
        {
            var filter = new FlightFilter { FilterType = FilterType.Date, DateStart = DateTime.Now, DateEnd = DateTime.Now.AddDays(1) };
            filter.IsValid().Should().BeTrue();
        }
    #endregion

    #region Numbers
        [Test]
        public void ShouldReturnAnInvalidFilterOnInvalidFlightStartMissing()
        {
            var filter = new FlightFilter { FilterType = FilterType.Number, FlightEnd = 0 };
            filter.IsValid().Should().BeFalse();
        }

        [Test]
        public void ShouldReturnAnInvalidFilterOnInvalidFlightEndMissing()
        {
            var filter = new FlightFilter { FilterType = FilterType.Number, FlightStart = 0 };
            filter.IsValid().Should().BeFalse();
        }

        [Test]
        public void ShouldReturnAnInvalidFilterOnInvalidFlightStartEqualFlightEnd()
        {
            var filter = new FlightFilter { FilterType = FilterType.Number, FlightStart = 1, FlightEnd = 1};
            filter.IsValid().Should().BeFalse();
        }

        [Test]
        public void ShouldReturnAValidFilterOnFlightStartLessThanFlightEnd()
        {
            var filter = new FlightFilter { FilterType = FilterType.Number, FlightStart = 1, FlightEnd = 2 };
            filter.IsValid().Should().BeTrue();
        }
    #endregion

    #region Aircraft/Airfield
        [Test]
        public void ShouldReturnAnInvalidFilterOnAircraftMissing()
        {
            var filter = new FlightFilter {FilterType = FilterType.Aircraft};
            filter.IsValid().Should().BeFalse();
        }
        


        [Test]
        public void ShouldReturnAnInvalidFilterOnAirfieldMissing()
        {
            var filter = new FlightFilter { FilterType = FilterType.Airfield };
            filter.IsValid().Should().BeFalse();
        }

        #endregion

    #region Crew

        [Test]
        [TestCase(-1)]
        [TestCase(4)]
        public void ShouldReturnAnInvalidFilterOnInvalidCrewPosition(int position)
        {
            var filter = new FlightFilter {FilterType = FilterType.Crew, Crew = position};
            filter.IsValid().Should().BeFalse();
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void ShouldReturnValidFilterOnValidCrewPosition(int position)
        {
            var filter = new FlightFilter { FilterType = FilterType.Crew, Crew = position };
            filter.IsValid().Should().BeTrue();
        }
    #endregion

    #region Launch

        [Test]
        public void ShouldReturnAnInvalidFilterOnMissingLaunchType()
        {
            var filter = new FlightFilter {FilterType = FilterType.Launch};
            filter.IsValid().Should().BeFalse();
        }

        [Test]
        [TestCase("A")]
        [TestCase("W")]
        public void ShouldReturnAValidFilterOnSuppliedLaunchType(string launchType)
        {
            var filter = new FlightFilter {FilterType = FilterType.Launch, Launch = launchType};
            filter.IsValid().Should().BeTrue();
        }
    #endregion
    }
}
