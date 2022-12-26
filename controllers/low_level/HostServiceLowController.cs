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
    internal class HostServiceLowController
    {
        private static Storage _db;
        private static PerstRoot _root;

        public HostServiceLowController(Storage db, PerstRoot root)
        {
            _db = db;
            _root = root;
        }

        /// <summary>
        /// Обновление объекта HostServiceModel
        /// </summary>
        public string update(string obj)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            var data = JsonConvert.DeserializeObject<HostServiceOutputModel>(obj);
            try
            {
                var hostService = (HostServiceModel)_root.idxHostService[data.Id];

                if (hostService == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта HostServiceModel с id={data.Id} не существует в ООБД")
                    );
                }

                var service = (ServiceModel)_root.idxService[data.ServiceId];

                if (service == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта DataSourceModel с id={data.ServiceId} не существует в ООБД")
                    );
                }

                var host = (HostModel)_root.idxHost[data.HostId];

                if (service == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта DataSourceModel с id={data.HostId} не существует в ООБД")
                    );
                }

                var oldService = (ServiceModel)_root.idxService[hostService.Service.Id];
                oldService.HostServiceLink.Remove(hostService);

                var oldHost = (HostModel)_root.idxHost[hostService.Host.Id];
                oldHost.HostServiceLink.Remove(hostService);

                service.HostServiceLink.Add(hostService);
                hostService.Service = service;

                host.HostServiceLink.Add(hostService);
                hostService.Host = host;

                hostService.Modify();
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Создание объекта HostServiceModel
        /// </summary>
        public string create(string obj)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            var data = JsonConvert.DeserializeObject<HostServiceOutputModel>(obj);
            try
            {
                var service = (ServiceModel)_root.idxService[data.ServiceId];

                if (service == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта ServiceModel с id={data.ServiceId} не существует в ООБД")
                    );
                }

                var host = (HostModel)_root.idxHost[data.HostId];
                if (host == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта HostModel с id={data.ServiceId} не существует в ООБД")
                    );
                }

                data.Id = Guid.NewGuid().ToString();
                var hostService = new HostServiceModel(
                    data.Id,
                    service,
                    host
                );

                service.HostServiceLink.Add(hostService);
                host.HostServiceLink.Add(hostService);

                _root.idxHostService.Put(hostService);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Получение всех объектов HostService
        /// </summary>
        public string getAll()
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                HostServiceOutputModel[] items = new HostServiceOutputModel[_root.idxHostService.Count];

                for (var i = 0; i < _root.idxHostService.Count; i++)
                {
                    HostServiceModel item = (HostServiceModel)_root.idxHostService.GetAt(i);
                    items[i] = new HostServiceOutputModel(
                        item.Id,
                        item.Host.Id,
                        item.Service.Id
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
        /// Получение конкретного объекта HostServiceModel
        /// </summary>
        public string get(string id)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                HostServiceOutputModel service = null;

                for (var i = 0; i < _root.idxHostService.Count; i++)
                {
                    HostServiceModel item = (HostServiceModel)_root.idxHostService.GetAt(i);
                    if (item.Id == id)
                    {
                        service = new HostServiceOutputModel(
                            item.Id,
                            item.Host.Id,
                            item.Service.Id
                        );
                        break;
                    }
                }

                return JsonConvert.SerializeObject(service);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }
        }

        /// <summary>
        /// Удаление объекта HostServiceModel
        /// </summary>
        public string delete(string id)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            HostServiceOutputModel dataOutput;

            try
            {
                HostServiceModel service = (HostServiceModel)_root.idxHostService[id];

                if (service == null)
                {
                    return JsonConvert.SerializeObject(new MessageModel("Объекта по данному ID нет в БД"));
                }

                dataOutput = new HostServiceOutputModel(
                    service.Id,
                    service.Host.Id,
                    service.Service.Id
                );

                service.Delete(_root);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(dataOutput);
        }
    }
}
