using System;
using System.Collections;
using System.Collections.Generic;
using BlueTrain.Shared;

namespace BlueTrain.Containers
{
    public class RoutingSlip
    {
        public readonly ContainerInformation ContainerInformation;
        public List<Trip> Trips;

        public ITerminalInformation GetNextDestination()
        {
            throw new NotImplementedException();
        }
    }
}
