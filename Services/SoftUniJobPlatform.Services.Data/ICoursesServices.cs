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

        T GetById<T>(int id);

        Course GetById(int id);

        Task Create(string userId, string title, string description,int categoryId, string imageUrl,string progress);

        Task DeleteAsync(int id);


        void AddCourseAsync(int courseId, string userId, double rate, int credit);
    }
}
