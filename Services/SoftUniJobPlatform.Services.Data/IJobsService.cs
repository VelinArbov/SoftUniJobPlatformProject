namespace SoftUniJobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
    using SoftUniJobPlatform.Data.Models;

    public interface IJobsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        public Task<int> CreateJob(string companyId, string title, string description, string position, int categoryId, string level, string location, int salary, string engagement);

        IEnumerable<T> GetById<T>(string id);

        T GetJobById<T>(int id);

        Job JobById(int id);

        Task EditAsync(int id, string position, string location, string jobRequirements, string engagement, int salary);

        Task DeleteAsync(int id);
    }
}
