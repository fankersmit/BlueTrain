using System.Linq;
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
            var container = CreateContainer();
            var  name = new Name("Alpha");
            var departedFromStation = CreateStation(name);
            
            var  name2 = new Name("Kappa");
            var arrivedAtStation = CreateStation(name2);
            
            //NOTE: act of arriving is not yet implemented
            var arrival = new Arrival( container, departedFromStation );
            Assert.True(arrivedAtStation.Arrivals.Count == 0);
            arrivedAtStation.Arrivals.Add(arrival);
            Assert.True(arrivedAtStation.Arrivals.Count == 1);
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
        
        [Fact]
        public void Station_SendTo_Adds_Container_ToDepartures()
        {
            Assert.False(true);
        }
        
        // private factory methods
        private BaseStation CreateStation()
        {
            return new BaseStation(new Name("StationName"), new Description("StationDescription"));
        }
        
        private BaseStation CreateStation(Name stationName)
        {
            return new BaseStation(stationName, new Description("StationDescription"));
        }

        private Container CreateContainer()
        {
            return new Container( 
                new Name("TestContainer"), 
                new Description("Test container description"));    
        }
    }
}