using System;
using Xunit;
using BlueTrain.Terminal;
using BlueTrain.Shared;


namespace BlueTrainTests
{
    public class TerminalTests
    {
        private readonly string _terminalName;
        private readonly string _terminalDescription;
        private readonly Guid _Id;

        
        public TerminalTests()
        {
            _terminalName = "terminalName";
            _terminalDescription = "terminalDescription";
            _Id = Guid.NewGuid();
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
        private Terminal CreateTerminal()
        {
            return new Terminal( _terminalName, _terminalDescription, _Id);
        }

        private TestTerminal CreateTestTerminal( TerminalStatus status)
        {
            return new TestTerminal( _terminalName, _terminalDescription, _Id, status);
        }

        class TestTerminal : Terminal
        {
            public TestTerminal(string name, string description, Guid Identifier, TerminalStatus status)
                : base( name,  description,  Identifier)
            {
                Status = status;
            }
        }
    }
}
