namespace SoftUniJobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SoftUniJobPlatform.Data.Models;

    public interface ICoursesService
    {
        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetAllByUserId<T>(string id);

        IQueryable<Course> GetCategories();

        T GetByName<T>(string name);

        T GetById<T>(int id);

        Course GetById(int id);

        void Create(string userId, string title, string description,int categoryId, string imageUrl,string progress);

        Task DeleteAsync(int id);

        Task EditAsync(int id, string title, string description, string imageUrl);

        void AddCourseAsync(int courseId, string userId, double rate, int credit);
    }
}
