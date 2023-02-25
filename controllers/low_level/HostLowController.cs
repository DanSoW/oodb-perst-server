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
    /// <summary>
    /// Класс низкоуровневого контроллера для коллекции объектов Host
    /// </summary>
    internal class HostLowController : IBaseLowController
    {
        private static Storage _db;
        private static PerstRoot _root;

        public HostLowController(Storage db, PerstRoot root)
        {
            _db = db;
            _root = root;
        }

        /// <summary>
        /// Обновление объекта HostModel
        /// </summary>
        public string update(string obj)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            var data = JsonConvert.DeserializeObject<HostOutputModel>(obj);
            try
            {
                HostModel host = (HostModel)_root.idxHost[data.Id];

                if (host == null)
                {
                    return JsonConvert.SerializeObject(new MessageModel("Объекта по данному ID нет в БД"));
                }

                host.Url = data.Url;
                host.Name = data.Name;
                host.IPv4 = data.IPv4;
                host.System = data.System;

                // Обновление данных
                host.Modify();
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Создание объекта HostModel
        /// </summary>
        public string create(string obj)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            var data = JsonConvert.DeserializeObject<HostOutputModel>(obj);
            try
            {
                data.Id = Guid.NewGuid().ToString();
                _root.idxHost.Put(new HostModel(data.Id, data.Name, data.Url, data.IPv4, data.System, _db.CreateLink(), _db.CreateLink()));
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Получение всех объектов AdminModel
        /// </summary>
        public string getAll()
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                HostOutputModel[] items = new HostOutputModel[_root.idxHost.Count];

                for (var i = 0; i < _root.idxHost.Count; i++)
                {
                    HostModel item = (HostModel)_root.idxHost.GetAt(i);
                    items[i] = new HostOutputModel(
                        item.Id,
                        item.Name,
                        item.Url,
                        item.IPv4,
                        item.System
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
        /// Получение конкретного объекта HostModel
        /// </summary>
        public string get(string id)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                HostOutputModel host = null;

                for (var i = 0; i < _root.idxHost.Count; i++)
                {
                    HostModel item = (HostModel)_root.idxHost.GetAt(i);
                    if (item.Id == id)
                    {
                        host = new HostOutputModel(
                            item.Id,
                            item.Name,
                            item.Url,
                            item.IPv4,
                            item.System
                        );

                        break;
                    }
                }

                return JsonConvert.SerializeObject(host);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }
        }

        /// <summary>
        /// Удаление объекта HostModel
        /// </summary>
        public string delete(string id)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            HostOutputModel hostOutput;

            try
            {
                HostModel host = (HostModel)_root.idxHost[id];

                if (host == null)
                {
                    return JsonConvert.SerializeObject(new MessageModel("Объекта по данному ID нет в БД"));
                }

                hostOutput = new HostOutputModel(
                    host.Id,
                    host.Name,
                    host.Url,
                    host.IPv4,
                    host.System
                );

                host.CascadeDelete(_root);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(hostOutput);
        }
    }
}
