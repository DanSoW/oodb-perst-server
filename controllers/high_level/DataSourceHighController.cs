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
    /// <summary>
    /// Обработчик верхнего уровня для коллекции объектов DataSource
    /// </summary>
    public class DataSourceHighController : BaseHighController
    {
        public DataSourceHighController(Storage db, PerstRoot root) : base(new DataSourceLowController(db, root)) { }
    }
}
