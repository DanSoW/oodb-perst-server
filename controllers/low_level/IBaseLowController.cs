using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.controllers.low_level
{
    /// <summary>
    /// Интерфейс базового контроллера
    /// </summary>
    public interface IBaseLowController
    {
        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="obj">Новые данные объекта</param>
        /// <returns>Данные обновлённого объекта</returns>
        string update(string obj);

        /// <summary>
        /// Создание объкета
        /// </summary>
        /// <param name="obj">Данные объекта</param>
        /// <returns>Данные созданного объекта</returns>
        string create(string obj);

        /// <summary>
        /// Получение списка объектов
        /// </summary>
        /// <returns>Список объектов</returns>
        string getAll();

        /// <summary>
        /// Получение конкретного объекта
        /// </summary>
        /// <param name="id">Идентификатор конкретного объекта</param>
        /// <returns>Данные объекта</returns>
        string get(string id);

        /// <summary>
        /// Удаление конкретного объекта
        /// </summary>
        /// <param name="obj">Данные объекта для удаления</param>
        /// <returns>Удалённый объект</returns>
        string delete(string obj);
    }
}
