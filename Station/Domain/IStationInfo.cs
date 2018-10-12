using System;
using System.Collections.Generic;

namespace Station.Domain
{
    public interface IStationInfo
    {
        Guid Id { get; }
        Name Name { get; }
        Description Description { get;  }
        Dictionary<string, string> Capabilities { get;  }
        StationStatus Status { get; set; }
    }
}