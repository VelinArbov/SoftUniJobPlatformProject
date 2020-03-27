using SoftUniJobPlatform.Data.Models;
using System.Collections.Generic;

namespace SoftUniJobPlatform.Services.Data
{
    public interface IJobsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        void CreateJob(string companyId, string title, string description,string categoryId,string level, string location,decimal salary,string engagement);

    }
}
