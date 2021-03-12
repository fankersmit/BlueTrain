using System;
using BlueTrain.Shared;

namespace BlueTrain.Containers
{
    public interface IContainerInfo
    {
        Guid Id { get; }
        Name Name { get; }
        Description Description { get;  }
        ContainerStatus Status { get; set; }

    }
}
