using System.Collections.Generic;

namespace SoftUniJobPlatform.Services.Data
{
    public interface IApplicationUsersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
