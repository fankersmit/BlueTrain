using  System;
using System.Collections.Generic;

namespace BlueTrain.Terminal
{
    public interface ITerminal
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Dictionary<string, string> Capabilities { get; }
        public TerminalStatus Status { get;  }
        public IHoldingYard HoldingYard { get;  }

        // methods
        public TerminalInformation GetTerminalInfo();
        public void Open();
        public void Close();
        public bool IsOpen();
        public bool IsClosed();


    }
}
