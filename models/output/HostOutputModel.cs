using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.models.output
{
    internal class HostOutputModel
    {
        public HostOutputModel()
        {
        }

        public HostOutputModel(string id, string name, string url, string iPv4, string system)
        {
            Id = id;
            Name = name;
            Url = url;
            IPv4 = iPv4;
            System = system;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string IPv4 { get; set; }
        public string System { get; set; }
    }
}
