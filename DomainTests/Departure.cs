using System;
using Station.Domain;

namespace DomainTests
{
    public class Departure
    {
        public IContainerInfo Container { get; }
        public DateTime ArrivedAt { get; }
        public IStationInfo DepartedTo { get;  }
    }
}