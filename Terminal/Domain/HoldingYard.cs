using System.Collections.Generic;

namespace Terminal.Domain
{
    public class HoldingYard
    {
        private readonly IList<Terminal.Domain.Container> _containers;

        // properties
        public bool IsEmpty => _containers.Count == 0;

        public int Count => _containers.Count;

        // ctors
        public HoldingYard()
        {
            _containers = new List<Terminal.Domain.Container>();
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