
using System.Collections.Generic;

namespace BlueTrain.Containers
{
    public class RoutingSlip
    {
        public readonly IContainerInfo ContainerInfo;
        public readonly IList<Destination> Destinations;
    }

    public class Destination
    {
    }
}
