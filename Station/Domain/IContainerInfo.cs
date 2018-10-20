using System;

namespace Station.Domain
{
    public interface IContainerInfo
    {
        Guid Id { get; }
        Name Name { get; }
        Description Description { get;  }
        ContainerStatus Status { get; set; }

    }
}