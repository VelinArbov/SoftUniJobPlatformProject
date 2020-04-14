using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftUniJobPlatform.Services.Data
{
    public interface IApplicationUsersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task DeleteAsync(string id);

        IEnumerable<T> GetAllStudents<T>(int? count = null);

        Task AddJobAsync(int jobId, string userId);
    }
}
