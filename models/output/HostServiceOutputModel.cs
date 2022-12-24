using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.models.output
{
    internal class HostServiceOutputModel
    {
        public HostServiceOutputModel()
        {
        }

        public HostServiceOutputModel(string id, string hostId, string serviceId)
        {
            Id = id;
            HostId = hostId;
            ServiceId = serviceId;
        }

        public string Id { get; set; }
        public string HostId { get; set; }
        public string ServiceId { get; set; }
    }
}
