namespace oodb_project.models.Host
{
    public class HostQuery
    {
        public HostQuery()
        {
        }

        public HostQuery(string iPv4, string system)
        {
            IPv4 = iPv4;
            System = system;
        }

        public string IPv4 { get; set; }
        public string System { get; set; }
    }
}
