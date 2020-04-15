namespace SoftUniJobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IApplicationUsersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task DeleteAsync(string id);

        IEnumerable<T> GetAllStudents<T>(int? count = null);

        Task AddJobAsync(int jobId, string userId);

        T GetStudentById<T>(string id);
    }
}
