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
    /// Класс низкоуровневого контроллера для коллекции объектов ServiceLow
    /// </summary>
    internal class ServiceLowController : IBaseLowController
    {
        private static Storage _db;
        private static PerstRoot _root;

        public ServiceLowController(Storage db, PerstRoot root)
        {
            _db = db;
            _root = root;
        }

        /// <summary>
        /// Обновление объекта ServiceModel
        /// </summary>
        public string update(string obj)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            var data = JsonConvert.DeserializeObject<ServiceOutputModel>(obj);
            try
            {
                var service = (ServiceModel)_root.idxService[data.Id];

                if (service == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта ServiceModel с id={data.Id} не существует в ООБД")
                    );
                }

                var dataSource = (DataSourceModel)_root.idxDataSource[data.DataSourceId];

                if (dataSource == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта DataSourceModel с id={data.DataSourceId} не существует в ООБД")
                    );
                }

                service.Name = data.Name;
                service.Port = data.Port;
                service.TimeUpdate = data.TimeUpdate;

                var oldDataSource = (DataSourceModel)_root.idxDataSource[service.DataSource.Id];
                oldDataSource.ServiceLink.Remove(service);

                dataSource.ServiceLink.Add(service);
                service.DataSource = dataSource;

                service.Modify();
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Создание объекта ServiceModel
        /// </summary>
        public string create(string obj)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            var data = JsonConvert.DeserializeObject<ServiceOutputModel>(obj);
            try
            {
                var dataSource = (DataSourceModel)_root.idxDataSource[data.DataSourceId];

                if (dataSource == null)
                {
                    return JsonConvert.SerializeObject(
                        new MessageModel($"Экземпляра объекта DataSourceModel с id={data.DataSourceId} не существует в ООБД")
                    );
                }

                data.Id = Guid.NewGuid().ToString();
                var service = new ServiceModel(
                        data.Id,
                        data.Name,
                        data.Port,
                        data.TimeUpdate,
                        dataSource,
                        _db.CreateLink()
                );

                // Добавление ServiceModel в ООБД
                _root.idxService.Put(service);

                // Добавление ссылки на ServiceModel в модель DataSourceModel
                dataSource.ServiceLink.Add(service);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Получение всех объектов ServiceModel
        /// </summary>
        public string getAll()
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                ServiceOutputModel[] items = new ServiceOutputModel[_root.idxService.Count];

                for (var i = 0; i < _root.idxService.Count; i++)
                {
                    ServiceModel item = (ServiceModel)_root.idxService.GetAt(i);

                    items[i] = new ServiceOutputModel(
                        item.Id,
                        item.Name,
                        item.Port,
                        item.TimeUpdate,
                        item.DataSource.Id
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
        /// Получение конкретного объекта ServiceModel
        /// </summary>
        public string get(string id)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                ServiceOutputModel service = null;

                for (var i = 0; i < _root.idxService.Count; i++)
                {
                    ServiceModel item = (ServiceModel)_root.idxService.GetAt(i);
                    if (item.Id == id)
                    {
                        service = new ServiceOutputModel(
                            item.Id,
                            item.Name,
                            item.Port,
                            item.TimeUpdate,
                            item.DataSource.Id
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
        /// Удаление объекта ServiceModel
        /// </summary>
        public string delete(string id)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            ServiceOutputModel serviceOutput;

            try
            {
                ServiceModel service = (ServiceModel)_root.idxService[id];

                if (service == null)
                {
                    return JsonConvert.SerializeObject(new MessageModel("Объекта по данному ID нет в БД"));
                }

                serviceOutput = new ServiceOutputModel(
                    service.Id,
                    service.Name,
                    service.Port,
                    service.TimeUpdate,
                    service.DataSource.Id
                );

                // Каскадное удаление
                service.CascadeDelete(_root);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(serviceOutput);
        }

        /// <summary>
        /// Получение списка объектов ServiceModel по порту
        /// </summary>
        public string getByPort(string port)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                // Использование JSQL
                var items = _root.idxService.Select(typeof(ServiceModel), $"Port = {port}");
                List<ServiceOutputModel> itemsOutput = new List<ServiceOutputModel>();

                foreach (var item in items)
                {
                    ServiceModel data = (ServiceModel)item;

                    itemsOutput.Add(new ServiceOutputModel(
                        data.Id,
                        data.Name,
                        data.Port,
                        data.TimeUpdate,
                        data.DataSource.Id
                    ));
                }

                return JsonConvert.SerializeObject(itemsOutput);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }
        }
    }
}
