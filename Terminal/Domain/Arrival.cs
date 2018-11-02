using System;
using Terminal.Domain;

namespace Terminal.Domain
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