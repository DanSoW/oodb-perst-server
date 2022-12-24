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

        public HostServiceModel(string id, string hostId, string serviceId)
        {
            Id = id;
            HostId = hostId;
            ServiceId = serviceId;
        }

        public string Id { get; set; }
        public string HostId { get; set; }
        public string ServiceId { get; set; }
    }
}
