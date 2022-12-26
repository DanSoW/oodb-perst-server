using ConsoleApp1.root;
using Perst;

namespace oodb_project.models
{
    /// <summary>
    /// Модель, характеризующая отдельный сервис (микросервис)
    /// </summary>
    public class ServiceModel : Persistent
    {
        public ServiceModel()
        {
        }

        public ServiceModel(string id, string name, int port, int timeUpdate, DataSourceModel dataSource, Link hostServiceLink)
        {
            Id = id;
            Name = name;
            Port = port;
            TimeUpdate = timeUpdate;
            DataSource = dataSource;
            HostServiceLink = hostServiceLink;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Port { get; set; }
        public int TimeUpdate { get; set; }
        public Link HostServiceLink { get; set; }
        public DataSourceModel DataSource { get; set; }

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

            // Удаляем из экземпляра объёкта DataSource ссылку на текущий элемент
            DataSource.ServiceLink.Remove(this);
            return root.idxService.Remove(this);
        }
    }
}
