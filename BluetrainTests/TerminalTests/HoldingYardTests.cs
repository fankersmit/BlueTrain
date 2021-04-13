using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using BlueTrain.Terminal;
using BlueTrain.Shared;
using BlueTrain.Containers;

namespace BlueTrainTests
{
    public class HoldingYardTests
    {
        private readonly string _terminalName;
        private readonly string _terminalDescription;
        private readonly Guid _terminalId;
        private readonly Uri _terminalUri;

        public HoldingYardTests()
        {
            _terminalUri = new Uri("http://bluertrain.nl/api");
            _terminalName = "terminalName";
            _terminalDescription = "terminalDescription";
            _terminalId = Guid.NewGuid();
        }

        [Fact]
        public void Adding_Beyond_Capacity_Throws_InvalidOperation_Exception()
        {
            // arrange
            var pattern  = new Regex("Holdingyard is filled to capacity: cannot add container with ID: [0-9a-z-]+\\.");
            var capacity = 5;
            var yard = new HoldingYard(capacity);

            // fill up the yard
            for (var i = 0; i < capacity; i++)
            {
                yard.Add(CreateContainer());
            }

            // assert
            var  ex  = Assert.Throws<InvalidOperationException>( ()=> yard.Add(CreateContainer()));
            Assert.Matches(pattern, ex.Message);
        }

        [Fact]
        public void IsFilled_True_When_Capacity_Reached()
        {
            // arrange
            var yard = new HoldingYard();
            var capacity = yard.Capacity;

            for (var i = 0; i < capacity - 3 ; i++)
            {
                yard.Add(CreateContainer());
            }
            var expectedResults = new bool[] { false,false, true, true, true, true };

            // act, assert
            foreach (var expected in expectedResults)
            {
                yard.Add(CreateContainer());
                Assert.Equal(expected, yard.IsFilled);
            }
        }

        [Fact]
        public void FindByInfo_Retrieves_Container_Using_ID()
        {
            // arrange
            var yard = new HoldingYard();
            var container = CreateContainer();
            ContainerInformation ci = container.Information();
            yard.Add(container);

            // act
            var ctr = yard.FindByInfo(ci);

            // assert
            Assert.Equal(container.Id, ctr.Id);
        }

        [Fact]
        public void Adding_More_Than_Once_Throws_ArgumentException()
        {
            // arrange
            var pattern  = new Regex("Container with ID: [0-9a-z-]+ already in yard: Cannot add container\\.");
            var yard = new HoldingYard();
            var container = CreateContainer();
            yard.Add(container);

            // act
            var ex = Assert.Throws<ArgumentException>(() => yard.Add(container));
            // assert
            Assert.Matches(pattern, ex.Message);
        }

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(1, 1, true)]
        [InlineData(3, 1, false)]
        [InlineData(3, 2, false)]
        [InlineData(3, 3, true)]
        public void IsEmpty_Reflects_Contents_When_Removing( int addContainers, int removeContainers, bool expected )
        {
            // arrange
            var yard = new HoldingYard();
            var containers = new List<Container>(addContainers);

            // act
            for (var cnt = 0; cnt < addContainers; ++cnt)
            {
                containers.Add(CreateContainer());
                yard.Add(containers.Last());
            }

            for( var cnt = 0; cnt < removeContainers; cnt++)
            {
                yard.Remove(containers[cnt]);
            }

            // assert
            Assert.Equal(expected, yard.IsEmpty);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(3, false)]
        public void IsEmpty_Reflects_Contents_When_Adding( int addContainers, bool expected )
        {
            // arrange
            var yard = new HoldingYard();

            // act
            for (var cnt = 0; cnt < addContainers; ++cnt)
            {
                yard.Add(new Container(Guid.NewGuid(), "Name", "Description"));
            }

            // assert
            Assert.Equal(expected, yard.IsEmpty);
        }

        [Fact]
        public void New_Terminal_Has_Empty_HoldingYard()
        {
            // arrange
            var t1 = CreateTerminal();
            var expected = 0;

            // act
            var holdingYard = t1.HoldingYard;
            var actual = holdingYard.Count;
            var isEmptyActual = holdingYard.IsEmpty;

            // assert
            Assert.Equal(expected, actual);
            Assert.True( isEmptyActual);
        }

        // private helper methods
        private Terminal CreateTerminal()
        {
            return new Terminal(_terminalUri, _terminalName, _terminalDescription, _terminalId);
        }

        private Container CreateContainer()
        {
            return new Container(Guid.NewGuid(), "Name", "Description");
        }
    }
}
