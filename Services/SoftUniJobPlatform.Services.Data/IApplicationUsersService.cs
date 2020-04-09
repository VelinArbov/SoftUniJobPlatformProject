using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftUniJobPlatform.Services.Data
{
    public interface IApplicationUsersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task DeleteAsync(string id);
    }
}
