using System;
using Station.Domain;

namespace Station.Domain
{
    public class Departure
    {
        public IContainerInfo Container { get; }
        public DateTime ArrivedAt { get; }
        public IStationInfo DepartedTo { get;  }
    }
}