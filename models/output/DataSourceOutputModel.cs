using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.models.output
{
    internal class DataSourceOutputModel
    {
        public DataSourceOutputModel()
        {
        }

        public DataSourceOutputModel(string id, string name, string url)
        {
            Id = id;
            Name = name;
            Url = url;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
