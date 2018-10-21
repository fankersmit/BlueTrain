using System;
using Station.Domain;

namespace Station.Domain
{
    public class Arrival
    {
        public Arrival(Container container, BaseStation departedFromStation)
        {
            Container = container;
            DepartedFrom = departedFromStation as IStationInfo;
            ArrivedAt = DateTime.Now.ToUniversalTime();
        }

        public IContainerInfo Container { get;  }
        public DateTime ArrivedAt { get; }
        public IStationInfo DepartedFrom { get;  }
    }
}