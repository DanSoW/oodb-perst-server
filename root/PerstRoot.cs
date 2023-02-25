using oodb_project.models;
using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.root
{
    /// <summary>
    /// Класс, представляющий корневой элемент базы данных
    /// </summary>
    public class PerstRoot : Persistent
    {
        // Индексы для быстрого доступа к коллекциям объектов
        public FieldIndex idxAdmin;
        public FieldIndex idxHost;
        public FieldIndex idxMonitorApp;
        public FieldIndex idxDataSource;
        public FieldIndex idxHostService;
        public FieldIndex idxService;

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="db">Хранилище</param>
        public PerstRoot(Storage db) : base(db)
        {
            idxAdmin = db.CreateFieldIndex(typeof(AdminModel), "Id", true);
            idxHost = db.CreateFieldIndex(typeof(HostModel), "Id", true);
            idxMonitorApp = db.CreateFieldIndex(typeof(MonitorAppModel), "Id", true);
            idxDataSource = db.CreateFieldIndex(typeof(DataSourceModel), "Id", true);
            idxHostService = db.CreateFieldIndex(typeof(HostServiceModel), "Id", true);
            idxService = db.CreateFieldIndex(typeof(ServiceModel), "Id", true);
        }

        public PerstRoot()
        {

        }
    }
}
