using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.models.output
{
    internal class MonitorAppOutputModel
    {
        public MonitorAppOutputModel()
        {
        }

        public MonitorAppOutputModel(string id, string name, string url, string hostId, string adminId)
        {
            Id = id;
            Name = name;
            Url = url;
            HostId = hostId;
            AdminId = adminId;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string HostId { get; set; }
        public string AdminId { get; set; }
    }
}
