namespace SoftUniJobPlatform.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        void CreateCategory(string title, string description, string imageUrl);
    }
}
