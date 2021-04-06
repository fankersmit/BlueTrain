using System;
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

        // return null if not found
        // finding does not remove container from yard
        public Container Find(Container c)
        {
            return _containers.SingleOrDefault(ctr => ctr.Id == c.Id);
        }

        // return null if not found
        // finding does not remove container from yard
        public Container Find(Guid ID)
        {
            return _containers.SingleOrDefault(ctr => ctr.Id == ID);
        }

        // return null if not found
        // finding does not remove container from yard
        public Container FindByInfo(ContainerInformation containerInfo)
        {
            return _containers.SingleOrDefault(ctr => ctr.Id == containerInfo.Id);

        }

        public void Add(Container container)
        {
            var ctr = _containers.FirstOrDefault(c => c.Id == container.Id);
            var message = $"Container with ID: {container.Id} already in yard: Cannot add container.";

            if (ctr != null)
            {
                throw new ArgumentException(message);
            }
            _containers.Add(container);
        }

        public void Remove(Container container)
        {
            _containers.Remove(container);
        }
    }
}
