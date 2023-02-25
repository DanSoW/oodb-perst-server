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
    /// Класс низкоуровневого контроллера для коллекции объектов DataSource
    /// </summary>
    internal class DataSourceLowController : IBaseLowController
    {
        private static Storage _db;
        private static PerstRoot _root;

        public DataSourceLowController(Storage db, PerstRoot root)
        {
            _db = db;
            _root = root;
        }

        /// <summary>
        /// Обновление объекта DataSourceModel
        /// </summary>
        public string update(string obj)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            var data = JsonConvert.DeserializeObject<DataSourceOutputModel>(obj);
            try
            {
                DataSourceModel dataSource = (DataSourceModel)_root.idxDataSource[data.Id];

                if (dataSource == null)
                {
                    return JsonConvert.SerializeObject(new MessageModel("Объекта по данному ID нет в БД"));
                }

                dataSource.Name = data.Name;
                dataSource.Url = data.Url;

                // Обновление данных
                dataSource.Modify();
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Создание объекта DataSourceModel
        /// </summary>
        public string create(string obj)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            var data = JsonConvert.DeserializeObject<DataSourceOutputModel>(obj);

            try
            {
                data.Id = Guid.NewGuid().ToString();
                _root.idxDataSource.Put(new DataSourceModel(data.Id, data.Name, data.Url, _db.CreateLink()));
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Получение всех объектов DataSourceModel
        /// </summary>
        public string getAll()
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                DataSourceOutputModel[] items = new DataSourceOutputModel[_root.idxDataSource.Count];

                for (var i = 0; i < _root.idxDataSource.Count; i++)
                {
                    DataSourceModel item = (DataSourceModel)_root.idxDataSource.GetAt(i);
                    items[i] = new DataSourceOutputModel(
                        item.Id,
                        item.Name,
                        item.Url
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
        /// Получение конкретного объекта DataSourceModel
        /// </summary>
        public string get(string id)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                DataSourceOutputModel dataSource = null;

                for (var i = 0; i < _root.idxDataSource.Count; i++)
                {
                    DataSourceModel item = (DataSourceModel)_root.idxDataSource.GetAt(i);
                    if (item.Id == id)
                    {
                        dataSource = new DataSourceOutputModel(item.Id, item.Name, item.Url);
                        break;
                    }
                }

                return JsonConvert.SerializeObject(dataSource);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }
        }

        /// <summary>
        /// Удаление объекта DataSourceModel
        /// </summary>
        public string delete(string id)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            DataSourceOutputModel dataSourceOutput;

            try
            {
                DataSourceModel dataSource = (DataSourceModel)_root.idxDataSource[id];

                if (dataSource == null)
                {
                    return JsonConvert.SerializeObject(new MessageModel("Объекта по данному ID нет в БД"));
                }

                dataSourceOutput = new DataSourceOutputModel(
                    dataSource.Id,
                    dataSource.Name,
                    dataSource.Url
                );

                // Каскадное удаление
                dataSource.CascadeDelete(_root);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(dataSourceOutput);
        }
    }
}
