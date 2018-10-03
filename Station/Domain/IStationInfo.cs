using System;
using System.Collections.Generic;

namespace Station.Domain
{
    public interface IStationInfo
    {
        Guid Id { get; };
        string Name { get; };
        string Description { get;  }
        Dictionary<string, string> Capabilities { get;  }
        StationStatus Status { get; set; }
    }
}