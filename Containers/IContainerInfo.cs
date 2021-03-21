using System;
using BlueTrain.Shared;

namespace BlueTrain.Containers
{
    public interface IContainerInfo
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get;  }
        ContainerStatus Status { get; set; }

    }
}
