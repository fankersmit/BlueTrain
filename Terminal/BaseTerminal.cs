using System;
using System.Collections;
using System.Collections.Generic;
using BlueTrain.Shared;

namespace BlueTrain.Terminal
{
    public class BaseTerminal : ITerminalInfo
    {
        // ctor
        public BaseTerminal(Name name, Description description)
        {
            Name = name;
            Description = description;
            Id  = Guid.NewGuid();
            Status = TerminalStatus.Closed;
            Capabilities = new Dictionary<string, string>();
            
            // handling of containers
            HoldingYard = new HoldingYard();
            Arrivals = new List<Arrival>();
            Departures = new List<Departure>();
        }

        public Guid Id { get; }
        public Name Name { get; }
        public Description Description { get; }
        public Dictionary<string, string> Capabilities { get; }
        public TerminalStatus Status { get; set; }
        public HoldingYard HoldingYard { get;  }
        public IList<Arrival> Arrivals { get;  }
        public IList<Departure> Departures { get;  }
        
    }
}
