using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.models
{
    internal class HttpModel
    {
        public HttpModel()
        {
        }

        public HttpModel(string path, string payload)
        {
            Path = path;
            Payload = payload;
        }

        public string Path { get; set; }
        public string Payload { get; set; }
    }
}
