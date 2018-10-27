using System.Linq;
using Terminal.Domain;
using Xunit;

namespace DomainTests
{
    public class ContainerTerminalTests
    {
        [Fact]
        public void Terminal_HasNoContainers_AfterCreation()
        {
            var Terminal = CreateTerminal();
            Assert.Empty( Terminal.Containers);
        }

        [Fact]
        public void Terminal_Records_ContainerArrival()
        {   
            var container = CreateContainer();
            var  name = new Name("Alpha");
            var departedFromTerminal = CreateTerminal(name);
            
            var  name2 = new Name("Kappa");
            var arrivedAtTerminal = CreateTerminal(name2);
            
            //NOTE: act of arriving is not yet implemented
            var arrival = new Arrival( container, departedFromTerminal );
            Assert.True(arrivedAtTerminal.Arrivals.Count == 0);
            arrivedAtTerminal.Arrivals.Add(arrival);
            Assert.True(arrivedAtTerminal.Arrivals.Count == 1);
        }

        [Fact]
        public void Terminal_Records_ContainerDeparture()
        {
            Assert.False(true);
        }
        
        [Fact]
        public void Terminal_ByDefault_Processes_Container()
        {
            Assert.False(true);
        }
        
        // private factory methods
        private BaseTerminal CreateTerminal()
        {
            return new BaseTerminal(new Name("TerminalName"), new Description("TerminalDescription"));
        }
        
        private BaseTerminal CreateTerminal(Name terminalName)
        {
            return new BaseTerminal(terminalName, new Description("TerminalDescription"));
        }

        private Container CreateContainer()
        {
            return new Container( 
                new Name("TestContainer"), 
                new Description("Test container description"));    
        }
    }
}