using ConsoleApp1.constants;
using ConsoleApp1.controllers.low_level;
using ConsoleApp1.models;
using ConsoleApp1.root;
using Newtonsoft.Json;
using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ConsoleApp1.controllers.high_level
{
    internal class MonitorAppHighController : WebSocketBehavior
    {
        private MonitorAppLowController _controller;

        public MonitorAppHighController(Storage db, PerstRoot root)
        {
            _controller = new MonitorAppLowController(db, root);
        }

        public MonitorAppHighController() { }

        protected override void OnMessage(MessageEventArgs e)
        {
            HttpModel body = JsonConvert.DeserializeObject<HttpModel>(e.Data);

            if (body.Path == ApiPerstServiceUrl.GET_ALL)
            {
                Send(_controller.getAll());
                return;
            }

            Send(_controller.getAll());
        }
    }
}
