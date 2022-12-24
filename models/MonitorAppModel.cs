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

        public MonitorAppModel(string id, string name, string url, string hostId, string adminId)
        {
            Id = id;
            Name = name;
            Url = url;
            HostId = hostId;
            AdminId = adminId;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string HostId { get; set; }
        public string AdminId { get; set; }
    }
}
