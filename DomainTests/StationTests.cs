using System;
using Xunit;
using Station.Domain;


namespace DomainTests
{
    public class StationTests
    {
        [Fact]
        public void Station_IsClosed_OnCreation()
        {
            var station = CreateStation();
            
            Assert.True(station.Status == StationStatus.Closed);
            Assert.NotNull(station.Name);
            Assert.NotNull(station.Description);
        }

        [Fact]
        public void Station_HasZeroCapabilities_OnCreation()
        {
            var station = CreateStation();
            Assert.Empty(station.Capabilities);
        }

        [Fact]
        public void Station_Has_NonEmptyGuid_AfterCreation()
        {
            var station = CreateStation();
            
            Assert.IsType<Guid>(station.Id);
            Assert.False( station.Id == Guid.Empty);
        }

        // private factory methods
        private BaseStation CreateStation()
        {
            return new BaseStation(new Name("StationName"), new Description("StationDescription"));
        }
        

    }
}