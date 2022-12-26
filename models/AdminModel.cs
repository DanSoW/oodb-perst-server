using ConsoleApp1.root;
using Perst;
using System;

namespace oodb_project.models
{
    /// <summary>
    /// Модель, характеризующая администратора приложения для мониторинга
    /// </summary>
    public class AdminModel : Persistent
    {
        public AdminModel()
        {
        }

        public AdminModel(string id, string email, Link monitorAppLink)
        {
            Id = id;
            Email = email;
            MonitorAppLink = monitorAppLink;
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public Link MonitorAppLink { get; set; }

        /// <summary>
        /// Каскадное удаление текущего экземпляра объекта из ООБД
        /// </summary>
        /// <param name="root">Корневой элемент ООБД</param>
        /// <returns></returns>
        public bool CascadeDelete(PerstRoot root)
        {
            foreach (MonitorAppModel item in MonitorAppLink)
            {
                item.Delete(root);
            }

            return root.idxAdmin.Remove(this);
        }
    }
}
