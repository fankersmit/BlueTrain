using System.Collections.Generic;
using BlueTrain.Containers;

namespace BlueTrain.Terminal
{
    public class HoldingYard
    {
        private readonly IList<Container> _containers;

        // properties
        public bool IsEmpty => _containers.Count == 0;

        public int Count => _containers.Count;

        // ctors
        public HoldingYard()
        {
            _containers = new List<Container>();
        }


        public void Add(Container container)
        {
            _containers.Add(container);
        }

        public void Remove(Container container)
        {
            _containers.Remove(container);
        }
    }
}
