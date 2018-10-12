using Xunit;
using Station.Domain;


namespace DomainTests
{
    public class StationTests
    {
        [Fact]
        public void Station_IsClosed_OnCreation()
        {
            var a = CreateStation();
            
            Assert.True(a.Status == StationStatus.Closed);
            Assert.NotNull(a.Name);
            Assert.NotNull(a.Description);
        }
  
        
        // private factory methods
        private BaseStation CreateStation()
        {
            return new BaseStation(new Name("StationName"), new Description("StationDescription"));
            
        }
        

    }
}