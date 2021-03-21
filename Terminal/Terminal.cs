using System;
using System.Collections;
using System.Collections.Generic;
using BlueTrain.Shared;

namespace BlueTrain.Terminal
{
    public class Terminal : ITerminal
    {
        // ctor
        public Terminal(string  name, string description, Guid Identifier)
        {
            Name = name;
            Description = description;
            Id  = Identifier;
            Status = TerminalStatus.Closed;
            Capabilities = new Dictionary<string, string>();
            
            // handling of containers
            HoldingYard = new HoldingYard();
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Dictionary<string, string> Capabilities { get; }
        public TerminalStatus Status { get; set; }
        public IHoldingYard HoldingYard { get;  }
    }
}
