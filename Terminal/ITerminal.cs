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
        public TerminalStatus Status { get; set; }
        public IHoldingYard HoldingYard { get;  }
    }
}
