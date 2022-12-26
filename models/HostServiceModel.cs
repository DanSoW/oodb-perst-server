using ConsoleApp1.root;
using Perst;

namespace oodb_project.models
{
    /// <summary>
    /// Модель связки конкретных сервисов с конкретными хостами
    /// </summary>
    public class HostServiceModel : Persistent
    {
        public HostServiceModel()
        {
        }

        public HostServiceModel(string id, ServiceModel service, HostModel host)
        {
            Id = id;
            Service = service;
            Host = host;
        }

        public string Id { get; set; }
        public ServiceModel Service { get; set; }
        public HostModel Host { get; set; }

        /// <summary>
        /// Удаление текущего экземлпяра объекта из ООБД
        /// </summary>
        /// <param name="root">Корневой элемент ООБД</param>
        /// <returns></returns>
        public bool Delete(PerstRoot root)
        {
            Service.HostServiceLink.Remove(this);
            Host.HostServiceLink.Remove(this);

            return root.idxHostService.Remove(this);
        }
    }
}
