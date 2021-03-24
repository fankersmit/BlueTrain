using System.Collections.Generic;
using System.Linq;
using BlueTrain.Containers;
using BlueTrain.Shared;

namespace BlueTrain.Terminal
{
    public class HoldingYard : IHoldingYard
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

        // methods
        public Container Find(Container c)
        {
            return _containers.SingleOrDefault(ctr => ctr == c);
        }

        public Container FindByInfo(ContainerInformation containerInfo)
        {
            return _containers.SingleOrDefault(ctr => ctr.Id == containerInfo.Id);

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
