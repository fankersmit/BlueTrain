using System;
using BlueTrain.Containers;

namespace BlueTrain.Terminal
{
    public class Arrival
    {
        public Arrival(Container container  )
        {
            Container = container;
            ArrivedAt = DateTime.Now.ToUniversalTime();
        }

        public IContainerInfo Container { get;  }
        public DateTime ArrivedAt { get; }
    }
}
