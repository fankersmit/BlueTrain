using System;

namespace Api.Controllers
{
    public class TerminalSettings
    {
        public Uri Address { get; set; }
        public string Name { get; set;  }
        public string  Description { get; set;  }
        public Guid  Id { get; set;  }
    }
}
