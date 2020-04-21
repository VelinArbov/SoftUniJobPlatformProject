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
        private readonly IRepository<StudentJob> studentJobsRepository;
        private const string NoAvailableJobsError = "No available jobs.";
        private const string NoCategorywithId = "No category with id {0}.";
        private const string NoJobwithId = "No job with id {0}.";
        public JobsService(
                IDeletableEntityRepository<Job> jobRepository,
                IRepository<StudentJob> studentJobsRepository)
        {
            this.jobRepository = jobRepository;
            this.studentJobsRepository = studentJobsRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Job> query =
                this.jobRepository.All().OrderByDescending(x => x.CreatedOn);

            if (!query.Any())
            {
                throw new ArgumentNullException("NoAvailableJobsError");
            }
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
                Title = title == null ? "No Tittle" : title,
                Description = description == null ? "No description" : description,
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

            if (!query.Any())
            {
                throw new ArgumentNullException("No company with this ID.");
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetStudentJobByJobId<T>(int id)
        {
            IQueryable<StudentJob> query =
                this.studentJobsRepository.All().Where(x => x.JobId == id);


            return query.To<T>().ToList();
        }

        public IEnumerable<StudentJob> GetStudentJobsById(int id)
        {
            return this.studentJobsRepository.All().Where(x => x.JobId == id);
        }

        public IEnumerable<Job> SearchJob(string searchTerms)
        {
            if (string.IsNullOrWhiteSpace(searchTerms) || string.IsNullOrEmpty(searchTerms))
            {
                throw new ArgumentNullException("No correct search.");
            }

            IQueryable<Job> query =
                this.jobRepository.All().Where(x => x.Description.Contains(searchTerms));

            return query.ToList();
        }

        public IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0)
        {
            var query = this.jobRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.CategoryId == categoryId).Skip(skip);

            if (!query.Any())
            {
                throw new ArgumentNullException(string.Format(NoCategorywithId, categoryId));
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetJobById<T>(int id)
        {
            var job = this.jobRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            if (job == null)
            {
                throw new ArgumentNullException(string.Format(NoJobwithId, id));
            }

            return job;
        }

        public Job JobById(int id)
        {
            var job = this.jobRepository.All()
                .FirstOrDefault(x => x.Id == id);

            if (job == null)
            {
                throw new ArgumentNullException(string.Format(NoJobwithId, id));
            }

            return job;
        }

        public async Task DeleteAsync(int id)
        {
            var job = this.jobRepository.All().FirstOrDefault(x => x.Id == id);

            if (job == null)
            {
                throw new ArgumentNullException(string.Format(NoJobwithId, id));
            }

            var isExist = this.studentJobsRepository.All().Any(x => x.JobId == id);

            if (isExist)
            {
                foreach (var studentJob in this.studentJobsRepository.All().Where(x => x.JobId == id))
                {
                    this.studentJobsRepository.Delete(studentJob);
                }

                await this.studentJobsRepository.SaveChangesAsync();
            }

            this.jobRepository.Delete(job);

            await this.jobRepository.SaveChangesAsync();
        }

        public int GetCountByCategoryId(int categoryId)
        {
            var jobs = this.jobRepository.All().Count(x => x.CategoryId == categoryId);
            if (jobs != 0)
            {
                return jobs;
            }

            throw new ArgumentNullException(string.Format(NoCategorywithId, categoryId));
        }

        public int GetCountJobs()
        {
            return this.jobRepository.All().Count();
        }

        public async Task EditAsync(int id, string position, string location, string jobRequirements, string engagement, int? salary)
        {
            var job = this.jobRepository.All().FirstOrDefault(x => x.Id == id);
            job.Level = Enum.Parse<SeniorityType>(jobRequirements, true);
            job.Location = location;
            job.Engagement = Enum.Parse<EngagementType>(engagement, true);
            job.Description = jobRequirements == null ? job.Description : jobRequirements;
            job.Salary = salary == null ? job.Salary : salary;

            await this.jobRepository.SaveChangesAsync();
        }
    }
}
