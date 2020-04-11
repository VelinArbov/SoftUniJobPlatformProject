using System.Threading.Tasks;

namespace SoftUniJobPlatform.Services.Data
{
    using System.Collections.Generic;

    using SoftUniJobPlatform.Data.Models;

    public interface IJobsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        public Task<int> CreateJob(string companyId, string title, string description, string position, int categoryId, string level, string location, int salary, string engagement);

        IEnumerable<T> GetById<T>(string id);

        T GetJobById<T>(int id);

        Task EditAsync(int id, string title, string description,int salary);

        Task DeleteAsync(int id);
    }
}
