using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.models.output
{
    internal class ServiceOutputModel
    {
        public ServiceOutputModel()
        {
        }

        public ServiceOutputModel(string id, string name, int port, int timeUpdate, string dataSourceId)
        {
            Id = id;
            Name = name;
            Port = port;
            TimeUpdate = timeUpdate;
            DataSourceId = dataSourceId;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Port { get; set; }

        public int TimeUpdate { get; set; }

        public string DataSourceId { get; set; }
    }
}
