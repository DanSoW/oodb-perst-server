using ConsoleApp1.root;
using Perst;

namespace oodb_project.models
{
    /// <summary>
    /// Модель, характеризующая приложения для мониторинга состояния удалённых хостов
    /// </summary>
    public class MonitorAppModel : Persistent
    {
        public MonitorAppModel()
        {
        }

        public MonitorAppModel(string id, string name, string url, HostModel host, AdminModel admin)
        {
            Id = id;
            Name = name;
            Url = url;
            Host = host;
            Admin = admin;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public HostModel Host { get; set; }
        public AdminModel Admin { get; set; }

        /// <summary>
        /// Удаление текущего экземпляра объекта из ООБД
        /// </summary>
        /// <param name="root">Корневой элемент ООБД</param>
        /// <returns></returns>
        public bool Delete(PerstRoot root)
        {
            Host.MonitorAppLink.Remove(this);
            Admin.MonitorAppLink.Remove(this);

            return root.idxMonitorApp.Remove(this);
        }
    }
}
