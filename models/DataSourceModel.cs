using ConsoleApp1.root;
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

        public DataSourceModel(string id, string name, string url, Link serviceLink)
        {
            Id = id;
            Name = name;
            Url = url;
            ServiceLink = serviceLink;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Link ServiceLink { get; set; }

        /// <summary>
        /// Каскадное удаление текущего экземпляра объекта из ООБД
        /// </summary>
        /// <param name="root">Корневой элемент ООБД</param>
        /// <returns></returns>
        public bool CascadeDelete(PerstRoot root)
        {
            foreach(ServiceModel item in ServiceLink)
            {
                item.CascadeDelete(root);
            }

            return root.idxDataSource.Remove(this);
        }
    }
}
