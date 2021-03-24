using BlueTrain.Containers;
using BlueTrain.Shared;

namespace BlueTrain.Terminal
{
    public interface IHoldingYard
    {
        bool IsEmpty { get; }
        int Count { get; }
        void Add(Container container);
        void Remove(Container container);
        Container Find(Container container);
        Container FindByInfo(ContainerInformation containerInfo);
    }
}
