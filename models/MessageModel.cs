namespace oodb_project.models
{
    public class MessageModel
    {
        public MessageModel()
        {
        }

        public MessageModel(string message)
        {
            this.message = message;
        }

        public string message { get; set; }
    }
}
