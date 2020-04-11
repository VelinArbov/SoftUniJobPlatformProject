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

        public T GetJobById<T>(int id)
        {
            var job = this.jobRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return job;
        }

        public async Task DeleteAsync(int id)
        {
            var category = this.jobRepository.All().FirstOrDefault(x => x.Id == id);

            this.jobRepository.Delete(category);

            await this.jobRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string title, string description, int salary)
        {
            var job = this.jobRepository.All().FirstOrDefault(x => x.Id == id);

            job.Title = title == null ? job.Title : title;
            job.Description = description == null ? job.Description : description;
            job.Salary = salary == null ? job.Salary : salary;

            await this.jobRepository.SaveChangesAsync();
        }
    }
}
