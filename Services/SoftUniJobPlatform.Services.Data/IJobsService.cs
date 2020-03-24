
using SoftUniJobPlatform.Data.Models;
using System.Collections.Generic;

namespace SoftUniJobPlatform.Services.Data
{
    public interface IJobsService
    {
        IEnumerable<Job> GetAll();
    }
}
