using ConsoleApp1.constants;
using ConsoleApp1.controllers.low_level;
using ConsoleApp1.models;
using ConsoleApp1.root;
using Newtonsoft.Json;
using oodb_project.models;
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
    internal class DataSourceHighController : WebSocketBehavior
    {
        private DataSourceLowController _controller;

        public DataSourceHighController(Storage db, PerstRoot root)
        {
            _controller = new DataSourceLowController(db, root);
        }

        public DataSourceHighController() { }

        protected override void OnMessage(MessageEventArgs e)
        {
            InitSendRoute(JsonConvert.DeserializeObject<HttpModel>(e.Data));
        }

        public void InitSendRoute(HttpModel body)
        {
            if (body.Path == ApiPerstServiceUrl.GET_ALL)
            {
                Send(_controller.getAll());
                return;
            }
            else if (body.Path == ApiPerstServiceUrl.GET)
            {
                Send(_controller.get(body.Payload));
                return;
            }
            else if (body.Path == ApiPerstServiceUrl.CREATE)
            {
                Send(_controller.create(body.Payload));
                return;
            }
            else if (body.Path == ApiPerstServiceUrl.UPDATE)
            {
                Send(_controller.update(body.Payload));
                return;
            }
            else if (body.Path == ApiPerstServiceUrl.DELETE)
            {
                Send(_controller.delete(body.Payload));
                return;
            }

            Send(JsonConvert.SerializeObject(new MessageModel("Not found 404")));
        }
    }
}
