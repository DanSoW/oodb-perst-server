/*
 * Точка входа в сервис oodb-perst-server
 * **/

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
        /// <summary>
        /// Хранилище
        /// </summary>
        static private Storage _db;

        /// <summary>
        /// Корневой класс
        /// </summary>
        static private PerstRoot _root;

        /// <summary>
        /// Статический метод инициализации объектно-ориентированной базы данных
        /// </summary>
        static void initPerstDb()
        {
            // Создание нового хранилища
            _db = StorageFactory.Instance.CreateStorage();

            // Открытие файла базы данных для записи
            _db.Open("perst.dbs", 100);


            // Получение корневого класса
            if(_db.Root == null)
            {
                _db.Root = new PerstRoot(_db);
            }

            // Связка корневого класса с атрибутом текущего класса
            _root = (PerstRoot)_db.Root;
        }

        /// <summary>
        /// Инициализация верхнеуровнего контроллера для коллекции объектов Admin
        /// </summary>
        /// <returns>Экземпляр верхнеуровневого контроллера Admin</returns>
        static AdminHighController initAdminHighController()
        {
            return new AdminHighController(_db, _root);
        }

        /// <summary>
        /// Инициализация верхнеуровнего контроллера для коллекции объектов Host
        /// </summary>
        /// <returns>Экземпляр верхнеуровневого контроллера Host</returns>
        static HostHighController initHostHighController()
        {
            return new HostHighController(_db, _root);
        }

        /// <summary>
        /// Инициализация верхнеуровнего контроллера для коллекции объектов HostService
        /// </summary>
        /// <returns>Экземпляр верхнеуровневого контроллера HostService</returns>
        static HostServiceHighController initHostServiceHighController()
        {
            return new HostServiceHighController(_db, _root);
        }

        /// <summary>
        /// Инициализация верхнеуровнего контроллера для коллекции объектов MonitorApp
        /// </summary>
        /// <returns>Экземпляр верхнеуровневого контроллера MonitorApp</returns>
        static MonitorAppHighController initMonitorAppHighController()
        {
            return new MonitorAppHighController(_db, _root);
        }

        /// <summary>
        /// Инициализация верхнеуровнего контроллера для коллекции объектов DataSource
        /// </summary>
        /// <returns>Экземпляр верхнеуровневого контроллера DataSource</returns>
        static DataSourceHighController initDataSourceHighController()
        {
            return new DataSourceHighController(_db, _root);
        }

        /// <summary>
        /// Инициализация верхнеуровнего контроллера для коллекции объектов Service
        /// </summary>
        /// <returns>Экземпляр верхнеуровневого контроллера Service</returns>
        static ServiceHighController initServiceHighController()
        {
            return new ServiceHighController(_db, _root);
        }

        /// <summary>
        /// Точка входа в консольное приложение
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Инициализация БД Perst
            initPerstDb();

            // Наполнение БД тестовыми значениями
            var mockData = new MockData(_db, _root);
            mockData.generateData();

            // Прослушивание WebSocket-соединений
            var wssv = new WebSocketServer("ws://127.0.0.1");
            wssv.AddWebSocketService("/admin", initAdminHighController);
            wssv.AddWebSocketService("/host", initHostHighController);
            wssv.AddWebSocketService("/host-service", initHostServiceHighController);
            wssv.AddWebSocketService("/monitor-app", initMonitorAppHighController);
            wssv.AddWebSocketService("/data-source", initDataSourceHighController);
            wssv.AddWebSocketService("/service", initServiceHighController);

            // Начало прослушивания соединений
            wssv.Start();

            // Остановка работы консольного приложения
            Console.ReadKey(true);

            // Остановка прослушивания соединений
            wssv.Stop();

            // Закрытие соединения с базой данных
            _db.Close();
        }
    }
}
