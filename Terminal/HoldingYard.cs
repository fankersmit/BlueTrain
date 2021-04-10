using System;
using System.Collections.Generic;
using System.Linq;
using BlueTrain.Containers;
using BlueTrain.Shared;

namespace BlueTrain.Terminal
{
    public class HoldingYard : IHoldingYard
   {
        // fields
        private const int _capacity = 100;
        private readonly IList<Container> _containers;

        // properties
        public bool IsEmpty => _containers.Count == 0;
        public bool IsFilled => _containers.Count >= _capacity;
        public int Count => _containers.Count;
        public int Capacity => _capacity;

        // ctors
        public HoldingYard()
        {
            _containers = new List<Container>();
        }

        // methods
        public IHoldingYardInformation GetHoldingYardInfo()
        {
            return new HoldingYardInformation(_capacity, _containers.Count);
        }


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
