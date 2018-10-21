using System;
using System.Collections;
using System.Collections.Generic;

namespace Station.Domain
{
    public class BaseStation : IStationInfo
    {
        // ctor
        public BaseStation(Name name, Description description)
        {
            Name = name;
            Description = description;
            Id  = Guid.NewGuid();
            Status = StationStatus.Closed;
            Capabilities = new Dictionary<string, string>();
            
            // handling of containers
            Containers = new List<Container>();
            Arrivals = new List<Arrival>();
        }

        public Guid Id { get; }
        public Name Name { get; }
        public Description Description { get; }
        public Dictionary<string, string> Capabilities { get; }
        public StationStatus Status { get; set; }
        public IList<Container> Containers { get;  }
        public IList<Arrival> Arrivals { get;  }
    }
}