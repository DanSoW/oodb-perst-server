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
    internal class DataSourceLowController
    {
        private static Storage _db;
        private static PerstRoot _root;

        public DataSourceLowController(Storage db, PerstRoot root)
        {
            _db = db;
            _root = root;
        }

        /// <summary>
        /// Обновление объекта AdminModel
        /// </summary>
        /*public Func<AdminModel, IResult> update = (newData) =>
        {
            if (_db == null)
            {
                return Results.Json(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                AdminModel data = _db.Query<AdminModel>(value => value.Id == newData.Id)[0];
                data.Email = newData.Email;

                _db.Store(data);
            }
            catch (Exception e)
            {
                return Results.Json(new MessageModel(e.Message));
            }

            return Results.Json(newData);
        };*/

        /// <summary>
        /// Создание объекта AdminModel
        /// </summary>
        /*public Func<AdminModel, IResult> create = (data) =>
        {
            if (_db == null)
            {
                return Results.Json(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                // Автоматическая генерация UUID
                data.Id = Guid.NewGuid().ToString();

                // Сохранение модели в ООДБ
                _db.Store(data);
            }
            catch (Exception e)
            {
                return Results.Json(new MessageModel(e.Message));
            }

            return Results.Json(data);
        };*/

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
        /// Получение конкретного объекта AdminModel
        /// </summary>
        /*public Func<string, IResult> get = (id) =>
        {
            if (_db == null)
            {
                return Results.Json(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                // Получение конкретной модели
                AdminModel data = _db.Query<AdminModel>(value => value.Id == id)[0];

                return Results.Json(data);
            }
            catch (Exception)
            {
                return Results.Json(new MessageModel($"Модели с Id = {id} нет в ООБД"));
            }
        };*/

        /// <summary>
        /// Удаление объекта AdminModel
        /// </summary>
        /*public Func<string, IResult> delete = (id) =>
        {
            if (_db == null)
            {
                return Results.Json(new MessageModel("Подключение к ООБД отсутствует"));
            }

            try
            {
                // Получение конкретной модели
                AdminModel data = _db.Query<AdminModel>(value => value.Id == id)[0];
                _db.Delete(data);

                return Results.Json(data);
            }
            catch (Exception)
            {
                return Results.Json(new MessageModel($"Модели с Id = {id} нет в ООБД"));
            }
        };*/
    }
}
