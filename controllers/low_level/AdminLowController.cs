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
    /// Класс низкоуровневого контроллера для коллекции объектов Admin
    /// </summary>
    internal class AdminLowController : IBaseLowController
    {
        private static Storage _db;
        private static PerstRoot _root;

        public AdminLowController(Storage db, PerstRoot root)
        {
            _db = db;
            _root = root;
        }

        /// <summary>
        /// Обновление объекта AdminModel
        /// </summary>
        public string update(string obj)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            // Десериализация данных (выходная модель)
            var data = JsonConvert.DeserializeObject<AdminOutputModel>(obj);
            try
            {
                // Поиск объекта в коллекци объектов по id
                AdminModel admin = (AdminModel)_root.idxAdmin[data.Id];

                if(admin == null)
                {
                    return JsonConvert.SerializeObject(new MessageModel("Объекта по данному ID нет в БД"));
                }

                // Обновление данных в объекте
                admin.Email = data.Email;

                // Фиксирование изменений в БД
                admin.Modify();
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Создание объекта AdminModel
        /// </summary>
        public string create(string obj)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            var data = JsonConvert.DeserializeObject<AdminOutputModel>(obj);
            try
            {
                // Добавление id создаваемому объекту
                data.Id = Guid.NewGuid().ToString();

                // Добавление нового объекта в коллекцию объектов
                _root.idxAdmin.Put(new AdminModel(data.Id, data.Email, _db.CreateLink()));
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
                // Коллекция объектов
                AdminOutputModel[] items = new AdminOutputModel[_root.idxAdmin.Count];

                // Конвертация объектов из БД в объекты, удобные для конвертации в JSON-формат
                for(var i = 0; i < _root.idxAdmin.Count; i++)
                {
                    AdminModel item = (AdminModel)_root.idxAdmin.GetAt(i);
                    items[i] = new AdminOutputModel(item.Id, item.Email);
                }

                // Возвращение списка объектов
                return JsonConvert.SerializeObject(items);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }
        }

        /// <summary>
        /// Получение конкретного объекта AdminModel
        /// </summary>
        public string get(string id)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                AdminOutputModel admin = null;

                for (var i = 0; i < _root.idxAdmin.Count; i++)
                {
                    AdminModel item = (AdminModel)_root.idxAdmin.GetAt(i);
                    if(item.Id == id)
                    {
                        admin = new AdminOutputModel(item.Id, item.Email);
                        break;
                    }
                }

                return JsonConvert.SerializeObject(admin);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }
        }

        /// <summary>
        /// Удаление объекта AdminModel
        /// </summary>
        public string delete(string id)
        {
            if (_db == null)
            {
                return JsonConvert.SerializeObject(new MessageModel("Подключение к ООБД отсутствует"));
            }

            AdminOutputModel adminOutput;

            try
            {
                AdminModel admin = (AdminModel)_root.idxAdmin[id];

                if (admin == null)
                {
                    return JsonConvert.SerializeObject(new MessageModel("Объекта по данному ID нет в БД"));
                }

                adminOutput = new AdminOutputModel(
                    admin.Id,
                    admin.Email
                );

                // Каскадное удаление объекта
                admin.CascadeDelete(_root);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new MessageModel(e.Message));
            }

            return JsonConvert.SerializeObject(adminOutput);
        }
    }
}
