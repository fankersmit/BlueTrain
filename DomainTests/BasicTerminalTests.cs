using System;
using Xunit;
using Terminal.Domain;


namespace DomainTests
{
    public class BasicTerminalTests
    {
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

        // private factory methods
        private BaseTerminal CreateTerminal()
        {
            return new BaseTerminal(new Name("TerminalName"), new Description("TerminalDescription"));
        }
    }
}