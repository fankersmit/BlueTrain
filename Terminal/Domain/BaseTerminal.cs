using System;
using System.Collections;
using System.Collections.Generic;

namespace Terminal.Domain
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
            Containers = new List<Container>();
            Arrivals = new List<Arrival>();
            Departures = new List<Departure>();
        }

        public Guid Id { get; }
        public Name Name { get; }
        public Description Description { get; }
        public Dictionary<string, string> Capabilities { get; }
        public TerminalStatus Status { get; set; }
        public IList<Container> Containers { get;  }
        public IList<Arrival> Arrivals { get;  }
        public IList<Departure> Departures { get;  }
        
    }
}