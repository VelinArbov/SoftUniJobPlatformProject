
namespace SoftUniJobPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class JobsService : IJobsService
    {
        private readonly IDeletableEntityRepository<Job> jobRepository;

        public JobsService(
                IDeletableEntityRepository<Job> jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Job> query =
                this.jobRepository.All().OrderByDescending(x => x.CreatedOn);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<int> CreateJob(string companyId, string title, string description, string position, int categoryId, string level, string location, int salary, string engagement)
        {
            var job = new Job
            {
                ApplicationUserId = companyId,
                Title = title,
                Description = description,
                Position = position,
                Salary = salary,
                CategoryId = categoryId,
                Level = Enum.Parse<SeniorityType>(level, true),
                Engagement = Enum.Parse<EngagementType>(engagement, true),
                Location = location,
            };
            await this.jobRepository.AddAsync(job);
            await this.jobRepository.SaveChangesAsync();
            return job.Id;
        }

        public IEnumerable<T> GetById<T>(string id)
        {
            IQueryable<Job> query =
                this.jobRepository.All().Where(x => x.ApplicationUserId == id);

            return query.To<T>().ToList();
        }

        public Job GetJobById<T>(int jobid)
        {
            var query =
                   this.jobRepository.All().FirstOrDefault(x => x.Id == id);

            return query.To
        }
    }
}
