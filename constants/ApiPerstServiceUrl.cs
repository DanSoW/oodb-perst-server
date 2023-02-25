using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.constants
{
    /// <summary>
    /// Класс, содержащий статические поля, из которых складываются маршруты для совершения операций
    /// </summary>
    public class ApiPerstServiceUrl
    {
        static public string GET = "/get";
        static public string GET_ALL = "/get/all";
        static public string CREATE = "/save";
        static public string UPDATE = "/update";
        static public string DELETE = "/delete";
    }
}
