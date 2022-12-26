using ConsoleApp1.models.output;
using ConsoleApp1.root;
using Newtonsoft.Json;
using oodb_project.models;
using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.controllers.low_level
{
    internal class MonitorAppLowController
    {
        private static Storage _db;
        private static PerstRoot _root;

        public MonitorAppLowController(Storage db, PerstRoot root)
        {
            _db = db;
            _root = root;
        }

        /// <summary>
        /// Обновление объекта MonitorApp
        /// </summary>
        public string update(string obj)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            var data = JsonConvert.DeserializeObject<MonitorAppOutputModel>(obj);
            try
            {
                var monitorApp = (MonitorAppModel)_root.idxMonitorApp[data.Id];

                if (monitorApp == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта MonitorAppModel с id={data.Id} не существует в ООБД")
                    );
                }

                var host = (HostModel)_root.idxHost[data.HostId];

                if (host == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта HostModel с id={data.HostId} не существует в ООБД")
                    );
                }

                var admin = (AdminModel)_root.idxAdmin[data.AdminId];

                if (admin == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта AdminModel с id={data.AdminId} не существует в ООБД")
                    );
                }

                var oldAdmin = (AdminModel)_root.idxService[monitorApp.Admin.Id];
                oldAdmin.MonitorAppLink.Remove(monitorApp);

                var oldHost = (HostModel)_root.idxHost[monitorApp.Host.Id];
                oldHost.MonitorAppLink.Remove(monitorApp);

                admin.MonitorAppLink.Add(monitorApp);
                monitorApp.Admin = admin;

                host.MonitorAppLink.Add(monitorApp);
                monitorApp.Host = host;

                monitorApp.Modify();
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Создание объекта MonitorApp
        /// </summary>
        public string create(string obj)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            var data = JsonConvert.DeserializeObject<MonitorAppOutputModel>(obj);
            try
            {
                var host = (HostModel)_root.idxHost[data.HostId];

                if (host == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта HostModel с id={data.HostId} не существует в ООБД")
                    );
                }

                var admin = (AdminModel)_root.idxAdmin[data.AdminId];
                if (admin == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта AdminModel с id={data.AdminId} не существует в ООБД")
                    );
                }

                data.Id = Guid.NewGuid().ToString();
                var monitorApp = new MonitorAppModel(
                    data.Id,
                    data.Name,
                    data.Url,
                    host,
                    admin
                );

                host.MonitorAppLink.Add(monitorApp);
                admin.MonitorAppLink.Add(monitorApp);

                _root.idxMonitorApp.Put(monitorApp);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Получение всех объектов MonitorApp
        /// </summary>
        public string getAll()
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                MonitorAppOutputModel[] items = new MonitorAppOutputModel[_root.idxMonitorApp.Count];

                for (var i = 0; i < _root.idxMonitorApp.Count; i++)
                {
                    MonitorAppModel item = (MonitorAppModel)_root.idxMonitorApp.GetAt(i);
                    items[i] = new MonitorAppOutputModel(
                        item.Id,
                        item.Name,
                        item.Url,
                        item.Host.Id,
                        item.Admin.Id
                    );
                }

                return JsonConvert.SerializeObject(items);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }
        }

        /// <summary>
        /// Получение конкретного объекта MonitorAppModel
        /// </summary>
        public string get(string id)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                MonitorAppOutputModel monitorApp = null;

                for (var i = 0; i < _root.idxMonitorApp.Count; i++)
                {
                    MonitorAppModel item = (MonitorAppModel)_root.idxMonitorApp.GetAt(i);
                    
                    if (item.Id == id)
                    {
                        monitorApp = new MonitorAppOutputModel(
                            item.Id,
                            item.Name,
                            item.Url,
                            item.Host.Id,
                            item.Admin.Id
                        );
                        break;
                    }
                }

                return JsonConvert.SerializeObject(monitorApp);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }
        }

        /// <summary>
        /// Удаление объекта MonitorAppModel
        /// </summary>
        public string delete(string id)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            MonitorAppOutputModel outputData;

            try
            {
                MonitorAppModel monitorApp = (MonitorAppModel)_root.idxMonitorApp[id];

                if (monitorApp == null)
                {
                    return JsonConvert.SerializeObject(new MessageModel("Объекта по данному ID нет в БД"));
                }

                outputData = new MonitorAppOutputModel(
                    monitorApp.Id,
                    monitorApp.Name,
                    monitorApp.Url,
                    monitorApp.Host.Id,
                    monitorApp.Admin.Id
                );

                monitorApp.Delete(_root);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(outputData);
        }
    }
}
