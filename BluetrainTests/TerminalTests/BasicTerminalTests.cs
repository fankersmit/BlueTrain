using System;
using Xunit;
using BlueTrain.Terminal;
using BlueTrain.Shared;


namespace BluetrainTests
{
    public class BasicTerminalTests
    {
        private readonly Name _terminalName;
        private readonly Description _terminalDescription;
        
        public BasicTerminalTests()
        {
            _terminalName = new Name("terminalName");
            _terminalDescription = new Description("terminalDescription");
        }

        public void Dispose()
        {
            // required by XUnit        
        }

        [Fact]
        public void Terminal_IsClosed_OnCreation()
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
        private BaseTerminal CreateTerminal()
        {
            return new BaseTerminal( _terminalName, _terminalDescription);
        }
    }
}
