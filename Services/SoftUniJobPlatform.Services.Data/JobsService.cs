
namespace SoftUniJobPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class JobsService : IJobsService
    {
        private readonly IDeletableEntityRepository<Job> jobRepository;

        public JobsService(IDeletableEntityRepository<Job> jobRepository)
        {
            this.jobRepository = jobRepository;
        }


        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Job> query =
                this.jobRepository.All().OrderBy(x => x.Title);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public void CreateJob(string companyId, string title, string description, string categoryId, string level, string location,
            decimal salary, string engagement)
        {
            throw new NotImplementedException();
        }
    }
}
