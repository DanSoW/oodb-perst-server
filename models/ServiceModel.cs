namespace oodb_project.models
{
    /// <summary>
    /// Модель, характеризующая отдельный сервис (микросервис)
    /// </summary>
    public class ServiceModel
    {
        public ServiceModel()
        {
        }

        public ServiceModel(string id, string name, int port, int timeUpdate, string dataSourceId)
        {
            Id = id;
            Name = name;
            Port = port;
            TimeUpdate = timeUpdate;
            DataSourceId = dataSourceId;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Port { get; set; }

        public int TimeUpdate { get; set; }

        public string DataSourceId { get; set; }
    }
}
