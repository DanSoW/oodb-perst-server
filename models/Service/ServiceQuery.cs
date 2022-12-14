namespace oodb_project.models.Service
{
    public class ServiceQuery
    {
        public ServiceQuery()
        {
        }

        public ServiceQuery(int port)
        {
           this.port = port;
        }

        public int port { get; set; }
    }
}
