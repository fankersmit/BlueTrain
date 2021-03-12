using System;
using BlueTrain.Containers;
using BlueTrain.Shared;
using Xunit;

namespace BluetrainTests
{
    public class ContainerTests
    {
        [Fact]
        public void Container_IsEmpty_AfterCreation()
        {
            var container = CreateContainer();
            Assert.True( container.Status == ContainerStatus.Empty);
        }
        
        [Fact]
        public void Container_Has_NonEmptyGuid_AfterCreation()
        {
            var container = CreateContainer();
            
            Assert.IsType<Guid>(container.Id);
            Assert.False( container.Id == Guid.Empty);
        }

        // private factory method
        private Container CreateContainer()
        {
            return new Container(
                new Name("ContainerName"), 
                new Description("Container description")
            );
        }
    }
}
