using System;
using System.Collections.Generic;

namespace Station.Domain
{
    public class BaseStation : IStationInfo
    {
        // ctor
        public BaseStation(string name, string description)
        {
            Name = name;
            Description = description;
            Id  = Guid.NewGuid();
            Status = StationStatus.Closed;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Dictionary<string, string> Capabilities { get; }
        public StationStatus Status { get; set; }
    }
}