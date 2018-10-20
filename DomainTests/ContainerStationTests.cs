using Station.Domain;
using Xunit;

namespace DomainTests
{
    public class ContainerStationTests
    {
        [Fact]
        public void Station_HasNoContainers_AfterCreation()
        {
            var station = CreateStation();
            Assert.Empty( station.Containers);
        }

        [Fact]
        public void Station_Records_ContainerArrival()
        {
            Assert.False(true);
        }

        [Fact]
        public void Station_Records_ContainerDeparture()
        {
            Assert.False(true);
        }
        
        [Fact]
        public void Station_ByDefault_Processes_Container()
        {
            Assert.False(true);
        }
        
        
        // private factory methods
        private BaseStation CreateStation()
        {
            return new BaseStation(new Name("StationName"), new Description("StationDescription"));
        }
        
        private BaseStation CreateStation(string stationName)
        {
            return new BaseStation(new Name(stationName), new Description("StationDescription"));
        }

        private Container CreateContainer()
        {
            return new Container( 
                new Name("TestContainer"), 
                new Description("Test container description"));    
        }
    }
}