using System;
using BlueTrain.Containers;
using BlueTrain.Shared;

namespace BlueTrain.Terminal
{
    public interface IHoldingYard
    {
        bool IsEmpty { get; }
        bool IsFilled { get; }
        int Count { get; }
        int Capacity { get;  }

        void Add(Container container);
        void Remove(Container container);
        Container Find(Container container);
        Container Find(Guid containerID);
        Container FindByInfo(ContainerInformation containerInfo);
        IHoldingYardInformation GetHoldingYardInfo();
    }
}
