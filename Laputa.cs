using Newtonsoft.Json;
using oodb_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ConsoleApp1
{
    internal class Laputa : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine(e.Data);

            var msg = JsonConvert.SerializeObject(new AdminModel("123", "123"));

            Send(msg);
        }
    }
}
