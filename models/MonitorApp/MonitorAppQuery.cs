namespace oodb_project.models.MonitorApp
{
    public class MonitorAppQuery
    {
        public MonitorAppQuery()
        {
        }

        public MonitorAppQuery(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; set; }
        public string Url { get; set; }
    }
}
