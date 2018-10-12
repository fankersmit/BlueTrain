using System;
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
        }

        public Guid Id { get; }
        public Name Name { get; }
        public Description Description { get; }
        public Dictionary<string, string> Capabilities { get; }
        public StationStatus Status { get; set; }
    }
}