using System;
using Xunit;
using BlueTrain.Terminal;
using BlueTrain.Shared;
using BlueTrain.Containers;


namespace BlueTrainTests
{
    public class TerminalTests
    {
        private readonly string _terminalName;
        private readonly string _terminalDescription;
        private readonly Guid _terminalId;

        private readonly string _containerName;
        private readonly string _containerDescription;
        private readonly Guid _containerId;

        
        public TerminalTests()
        {
            _terminalName = "terminalName";
            _terminalDescription = "terminalDescription";
            _terminalId = Guid.NewGuid();

            _containerName = "containerName";
            _containerDescription = "containerDescription";
            _containerId = Guid.NewGuid();
        }

        [Fact]
        public void Send_Calls_Receive_On_Receiving_Terminal()
        {
            // arrange
            var t1 = CreateTerminal();
            var t2 = CreateTerminal(Guid.NewGuid());
            var c = CreateContainer();
            t1.Receive(c);
            int containersInHoldingYard = t2.HoldingYard.Count;  // 0

            // act
            t1.Send(c, t2);
            var actual = t2.HoldingYard.Count;

            // assert, t2.Receive was called
            Assert.True(actual == containersInHoldingYard + 1);
        }

        [Fact]
        public void Send_Removes_Container_FromHoldingYard()
        {
            // arrange
            var t1 = CreateTerminal();
            var t2 = CreateTerminal(Guid.NewGuid());
            var c = CreateContainer();
            t1.Receive(c);
            int containersInHoldingYard = t1.HoldingYard.Count;

            // act
            t1.Send(c, t2);
            var actual = t1.HoldingYard.Count;

            // assert
            Assert.True(actual == containersInHoldingYard - 1);
        }

        [Fact]
        public void Receive_Puts_Container_In_HoldingYard()
        {
            // arrange
            var t = CreateTerminal();
            var c = CreateContainer();
            int containersInHoldingYard = t.HoldingYard.Count;
            // act
            t.Receive(c);
            int actual = t.HoldingYard.Count;

            // assert
            Assert.True(actual == containersInHoldingYard + 1);
        }

        [Fact]
        public void Remove_Clears_Container_From_HoldingYard()
        {
            // arrange
            var t1 = CreateTerminal();
            var c = CreateContainer();
            t1.Receive(c);
            int containersInHoldingYard = t1.HoldingYard.Count;

            // act
            t1.Remove(c.Information());
            var actual = t1.HoldingYard.Count;

            // assert
            Assert.True(actual == containersInHoldingYard - 1);
        }

        [Fact]
        public void Ctor_Uses_TerminalDefinition()
        {
            // arrange
            var td = new TerminalDefinition
            {
                    Name = _terminalName,
                    Description = _terminalDescription,
                    ID = _terminalId,
            };
            var expected = true;

            // act
            var t = new Terminal(td);
            var actual = t.IsClosed();

            // assert
            Assert.Equal(actual,expected);
            Assert.Equal(t.Name, td.Name);
            Assert.Equal(t.Description, td.Description);
            Assert.Equal(t.Id, td.ID);
        }

        [Theory]
        [InlineData(TerminalStatus.Closed,true)]
        [InlineData(TerminalStatus.Open, false)]
        [InlineData(TerminalStatus.Opening, false)]
        [InlineData(TerminalStatus.Closing, false)]
        public void IsClosed(TerminalStatus initial, bool expected)
        {
            // arrange
            var terminal = CreateTestTerminal(initial);
            // act
            var actual = terminal.IsClosed();
            // assert
            Assert.Equal(actual,expected);
        }

        [Theory]
        [InlineData(TerminalStatus.Closed,false)]
        [InlineData(TerminalStatus.Open, true)]
        [InlineData(TerminalStatus.Opening, false)]
        [InlineData(TerminalStatus.Closing, false)]
        public void IsOpen(TerminalStatus initial, bool expected)
        {
            // arrange
            var terminal = CreateTestTerminal(initial);
            // act
            var actual = terminal.IsOpen();
            // assert
            Assert.Equal(actual,expected);
        }

        [Theory]
        [InlineData(TerminalStatus.Closed, TerminalStatus.Open)]
        [InlineData(TerminalStatus.Open, TerminalStatus.Open)]
        [InlineData(TerminalStatus.Opening, TerminalStatus.Opening)]
        [InlineData(TerminalStatus.Closing, TerminalStatus.Closing)]
        public void Open_Opens_Only_ClosedTerminal(TerminalStatus initial, TerminalStatus expected)
        {
            // arrange
            var terminal = CreateTestTerminal(initial);
            // act
            var current = terminal.Status;
            terminal.Open();
            var actualStatus = terminal.Status;
            // assert
            Assert.True(current == initial);
            Assert.True(actualStatus == expected);
        }

        [Theory]
        [InlineData(TerminalStatus.Open, TerminalStatus.Closed)]
        [InlineData(TerminalStatus.Closed, TerminalStatus.Closed)]
        [InlineData(TerminalStatus.Opening, TerminalStatus.Opening)]
        [InlineData(TerminalStatus.Closing, TerminalStatus.Closing)]
        public void Close_Closes_Only_OpenTerminal(TerminalStatus initial, TerminalStatus expected)
        {
            // arrange
            var terminal = CreateTestTerminal(initial);
            // act
            var current = terminal.Status;
            terminal.Close();
            var actualStatus = terminal.Status;
            // assert
            Assert.True(current == initial);
            Assert.True(actualStatus == expected);
        }


        [Fact]
        public void Information_IsFullyInitialized()
        {
            // arrange
            var terminal = CreateTerminal();
            // act
            var information = terminal.GetTerminalInfo();
            // assert
            Assert.Equal(information.Name, terminal.Name );
            Assert.Equal(information.Description, terminal.Description );
            Assert.Equal(information.ID, terminal.Id.ToString() );
            Assert.Equal(information.Status, Enum.GetName(terminal.Status) );
        }

        [Fact]
        public void Terminal_State_IsClosed_OnCreation()
        {
            var terminal = CreateTerminal();
            
            Assert.True(terminal.Status == TerminalStatus.Closed);
            Assert.NotNull(terminal.Name);
            Assert.NotNull(terminal.Description);
        }

        [Fact]
        public void Terminal_HasZeroCapabilities_OnCreation()
        {
            var terminal = CreateTerminal();
            Assert.Empty(terminal.Capabilities);
        }

        [Fact]
        public void Terminal_Has_NonEmptyGuid_AfterCreation()
        {
            var terminal = CreateTerminal();
            
            Assert.IsType<Guid>(terminal.Id);
            Assert.False( terminal.Id == Guid.Empty);
        }

        [Fact]
        public void Terminal_Yard_IsEmpty_AfterCreation()
        {
            var terminal = CreateTerminal();
            Assert.True(terminal.HoldingYard.IsEmpty);
        }

        // private factory methods
        #region private helper mthods
        private Container CreateContainer()
        {
            return new Container(_containerId, _containerName, _containerDescription);
        }

        private Terminal CreateTerminal( Guid ID)
        {
            return new Terminal( _terminalName, _terminalDescription, ID);
        }

        private Terminal CreateTerminal()
        {
            return new Terminal( _terminalName, _terminalDescription, _terminalId);
        }

        private TestTerminal CreateTestTerminal( TerminalStatus status)
        {
            return new TestTerminal( _terminalName, _terminalDescription, _terminalId, status);
        }

        class TestTerminal : Terminal
        {
            public TestTerminal(string name, string description, Guid Identifier, TerminalStatus status)
                : base( name,  description,  Identifier)
            {
                Status = status;
            }
        }
        #endregion
    }
}
