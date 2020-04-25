namespace SoftUniJobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoftUniJobPlatform.Data.Models;

    public interface IApplicationUsersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task DeleteAsync(string id);

        IEnumerable<T> GetAllStudents<T>(int? count = null);

        Task AddJobAsync(int jobId, string userId);

        Task AddSkillAsync(int skill, string userId);

        T GetStudentById<T>(string id);

        ApplicationUser GetStudentById(string id);

        int GetStudentsCount();

        int GetCompaniesCount();

    }
}
