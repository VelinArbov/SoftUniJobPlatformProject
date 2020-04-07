namespace SoftUniJobPlatform.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(int id);

        void Create(string title, string description, string imageUrl);

        void Delete(int id);
    }
}
