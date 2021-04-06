using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;
using FluentAssertions;
using BlueTrain.Containers;

namespace BlueTrainTests
{
    public class ContainerTests
    {
        [Fact]
        public void Can_Serialize_Container_To_Json()
        {
            // arrange
            var ctr = CreateContainer();
            var info = ctr.Information();

            // act
            string jsonString = ctr.ToJsonString();
            var ctr2 =  JsonSerializer.Deserialize<Container>(jsonString);

            // assert
            Assert.IsType<Container>(ctr2);
            // from fluent assertions
            ctr2.Should().BeEquivalentTo(ctr);

        }

        [Fact]
        public void Container_Has_CreatedOn_Initialized()
        {
            // arrange
            var startDateTime = DateTime.UtcNow;
            // act
            var container = CreateContainer();
            // assert
            Assert.False(DateTime.MinValue== container.CreatedOn); // not initialized
            Assert.True(startDateTime < container.CreatedOn); // wrongly initialized
        }

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
            var ID = Guid.NewGuid();
            return new Container( ID, "ContainerName","Container description");
        }
    }
}
