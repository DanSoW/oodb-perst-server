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
        static void Main(string[] args)
        {
            // Прослушивание WebSocket-соединения
            var wssv = new WebSocketServer("ws://127.0.0.1");

            // Добавление обработчика для сервиса Laputa
            wssv.AddWebSocketService<Laputa>("/Laputa");

            // Начало прослушивания соединений
            wssv.Start();

            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}
