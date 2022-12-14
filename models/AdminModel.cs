using System;

namespace oodb_project.models
{
    /// <summary>
    /// Модель, характеризующая администратора приложения для мониторинга
    /// </summary>
    [Serializable]
    public class AdminModel
    {
        public AdminModel()
        {
        }

        public AdminModel(string id, string email)
        {
            Id = id;
            Email = email;
        }

        public string Id { get; set; }
        public string Email { get; set; }
    }
}
