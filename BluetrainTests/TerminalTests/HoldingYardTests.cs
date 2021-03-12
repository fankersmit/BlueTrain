using Xunit;
using BlueTrain.Shared;
using BlueTrain.Containers;
using BlueTrain.Terminal;

namespace BluetrainTests
{
    public class HoldingYardTests
    {
        [Fact]
        public void Yard_IsEmpty_AfterCreation()
        {
            var yard = CreateHoldingYard();
            Assert.True( yard.IsEmpty);
        }

        [Fact]
        public void Yard_Counts_Containers_Added()
        {
            var yard = CreateHoldingYard();
            int containersInYard = yard.Count;

            yard.Add(CreateContainer());
            yard.Add(CreateContainer());
            int newCount = yard.Count;
            
            Assert.True( containersInYard + 2  == newCount);
        }
        
        [Fact]
        public void Yard_Counts_Containers_Removed()
        {
            var yard = CreateHoldingYard();
            var container = CreateContainer();
           
            yard.Add(container);
            yard.Add(CreateContainer());
            var containersInYard = yard.Count;
            yard.Remove(container);
            Assert.True(yard.Count == (containersInYard - 1));
        }

        // private helper methods
        private HoldingYard CreateHoldingYard()
        {
            return new HoldingYard();
        }

        private Container CreateContainer()
        {
            return new Container( new Name("Name"), new Description("Description"));
        }

    }
}
