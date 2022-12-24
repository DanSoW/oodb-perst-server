using ConsoleApp1.controllers.high_level;
using ConsoleApp1.data;
using ConsoleApp1.root;
using Perst;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ConsoleApp1
{
    internal class Program
    {
        static private Storage _db;
        static private PerstRoot _root;

        static void initPerstDb()
        {
            _db = StorageFactory.Instance.CreateStorage();
            _db.Open("perst.dbs", 100);

            if(_db.Root == null)
            {
                _db.Root = new PerstRoot(_db);
            }

            _root = (PerstRoot)_db.Root;
        }

        static AdminHighController initAdminHighController()
        {
            return new AdminHighController(_db, _root);
        }

        static HostHighController initHostHighController()
        {
            return new HostHighController(_db, _root);
        }

        static HostServiceHighController initHostServiceHighController()
        {
            return new HostServiceHighController(_db, _root);
        }

        static MonitorAppHighController initMonitorAppHighController()
        {
            return new MonitorAppHighController(_db, _root);
        }

        static DataSourceHighController initDataSourceHighController()
        {
            return new DataSourceHighController(_db, _root);
        }

        static ServiceHighController initServiceHighController()
        {
            return new ServiceHighController(_db, _root);
        }

        static void Main(string[] args)
        {
            // Инициализация БД Perst
            initPerstDb();

            // Наполнение БД тестовыми значениями
            var mockData = new MockData(_db, _root);
            mockData.generateData();

            // Прослушивание WebSocket-соединения
            var wssv = new WebSocketServer("ws://127.0.0.1");
            wssv.AddWebSocketService("/admin", initAdminHighController);
            wssv.AddWebSocketService("/host", initHostHighController);
            wssv.AddWebSocketService("/host-service", initHostServiceHighController);
            wssv.AddWebSocketService("/monitor-app", initMonitorAppHighController);
            wssv.AddWebSocketService("/data-source", initDataSourceHighController);
            wssv.AddWebSocketService("/service", initServiceHighController);

            // Начало прослушивания соединений
            wssv.Start();

            Console.ReadKey(true);

            wssv.Stop();
            _db.Close();
        }
    }
}
