using ConsoleApp1.constants;
using ConsoleApp1.controllers.low_level;
using ConsoleApp1.models;
using Newtonsoft.Json;
using oodb_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ConsoleApp1.controllers.high_level
{
    /// <summary>
    /// Абстрактный класс верхнеуровневых контроллеров
    /// </summary>
    public abstract class BaseHighController : WebSocketBehavior
    {
        // Ссылка на обработчик нижнего уровня
        protected IBaseLowController _controller;

        public BaseHighController(IBaseLowController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Метод, обрабатывающий приход сообщений
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMessage(MessageEventArgs e)
        {
            // Вызов функции InitSendRoute и передача ему десериализованных данных, полученных от вызывающей стороны
            InitSendRoute(JsonConvert.DeserializeObject<HttpModel>(e.Data));
        }

        /// <summary>
        /// Инициализация вызовов процедур, в зависимости от требуемого пути
        /// </summary>
        /// <param name="body">Данные, полученные от вызывающей стороны</param>
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
