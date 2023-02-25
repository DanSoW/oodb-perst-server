using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.models
{
    public class IndexModel
    {
        public IndexModel()
        {
        }

        public IndexModel(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
