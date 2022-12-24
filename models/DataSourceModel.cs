using Perst;

namespace oodb_project.models
{
    /// <summary>
    /// Модель, характеризующая источник данных для сервиса
    /// </summary>
    public class DataSourceModel : Persistent
    {
        public DataSourceModel()
        {
        }

        public DataSourceModel(string id, string name, string url)
        {
            Id = id;
            Name = name;
            Url = url;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
