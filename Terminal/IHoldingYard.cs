using BlueTrain.Containers;

namespace BlueTrain.Terminal
{
    public interface IHoldingYard
    {
        bool IsEmpty { get; }
        int Count { get; }
        void Add(Container container);
        void Remove(Container container);
    }
}