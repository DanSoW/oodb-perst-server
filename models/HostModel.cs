using ConsoleApp1.root;
using Perst;

namespace oodb_project.models
{
    /// <summary>
    /// Модель, характеризующая конкретный удалённый хост
    /// </summary>
    public class HostModel : Persistent
    {
        public HostModel()
        {
        }

        public HostModel(string id, string name, string url, string iPv4, string system, Link hostServiceLink, Link monitorAppLink)
        {
            Id = id;
            Name = name;
            Url = url;
            IPv4 = iPv4;
            System = system;
            HostServiceLink = hostServiceLink;
            MonitorAppLink = monitorAppLink;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string IPv4 { get; set; }
        public string System { get; set; }
        public Link HostServiceLink { get; set; }
        public Link MonitorAppLink { get; set; }

        /// <summary>
        /// Каскадное удаление текущего экземпляра объекта из ООБД
        /// </summary>
        /// <param name="root">Корневой элемент ООБД</param>
        /// <returns></returns>
        public bool CascadeDelete(PerstRoot root)
        {
            foreach (HostServiceModel item in HostServiceLink)
            {
                item.Delete(root);
            }

            foreach (MonitorAppModel item in MonitorAppLink)
            {
                item.Delete(root);
            }

            return root.idxHost.Remove(this);
        }
    }
}
