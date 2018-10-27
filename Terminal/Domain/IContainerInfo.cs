using System;

namespace Terminal.Domain
{
    public interface IContainerInfo
    {
        Guid Id { get; }
        Name Name { get; }
        Description Description { get;  }
        ContainerStatus Status { get; set; }

    }
}