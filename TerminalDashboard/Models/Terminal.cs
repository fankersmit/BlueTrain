using System;

namespace TerminalDashboard.Models
{
    public class Terminal
    {
        public Uri Address { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ID { get; set; }
        public string Status { get; set; }
        public DateTime InformationTimeStamp { get; set;  }
    }
}
