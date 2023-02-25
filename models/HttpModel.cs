using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.models
{
    /// <summary>
    /// Класс модели, которая используется для взаимодействия с другими сервисами
    /// </summary>
    public class HttpModel
    {
        public HttpModel()
        {
        }

        public HttpModel(string path, string payload)
        {
            Path = path;
            Payload = payload;
        }

        /// <summary>
        /// Путь, по которому отправить данные
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Отправляемые данные
        /// </summary>
        public string Payload { get; set; }
    }
}
