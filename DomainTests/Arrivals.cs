using System;
using Station.Domain;

namespace DomainTests
{
    public class Arrival
    {
        public IContainerInfo Container { get; }
        public DateTime ArrivedAt { get; }
        public IStationInfo DepartedFrom { get;  }
    }
}