using System;
using System.ComponentModel.Design;

namespace SoftUniJobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Data.Models.Enum;
    using SoftUniJobPlatform.Services.Mapping;

    public class ApplicationUsersService : IApplicationUsersService
    {
        private const string NoUserWithId = "No user with Id {0}";
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IJobsService jobsService;
        private readonly IRepository<StudentJob> studentJobRepository;

        public ApplicationUsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IJobsService jobsService,
            IRepository<StudentJob> studentJobRepository)

        {
            this.usersRepository = usersRepository;
            this.jobsService = jobsService;
            this.studentJobRepository = studentJobRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query =
                this.usersRepository.All()
                    .OrderByDescending(x => x.CreatedOn);

            if (!query.Any())
            {
                throw new ArgumentNullException("No users.");
            }

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task DeleteAsync(string id)
        {
            var user = this.usersRepository.All()
                .FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new ArgumentNullException(string.Format(NoUserWithId, id));
            }

            this.usersRepository.Delete(user);

            await this.usersRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllStudents<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query =
                this.usersRepository.All()
                    .Where(x => x.Type == UserType.Student && x.IsAdmin == false);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task AddJobAsync(int jobId, string userId)
        {
            var user = this.usersRepository.All()
                .FirstOrDefault(x => x.Id == userId);
            var job = this.jobsService.JobById(jobId);
            if (user == null || job == null)
            {
                throw new ArgumentNullException("Not correct input");
            }

            var exist = this.studentJobRepository.All().FirstOrDefault(x => x.JobId == jobId && x.ApplicationUserId == userId);
            if (exist == null)
            {
                var studentJob = new StudentJob
                {
                    ApplicationUserId = userId,
                    ApplicationUser = user,
                    JobId = jobId,
                    Job = job,
                };
                user.StudentJobs.Add(studentJob);

                await this.usersRepository.SaveChangesAsync();

            }
            else
            {
                throw new ArgumentNullException("Your candidate in this job offer");
            }
        }

        public T GetStudentById<T>(string id)
        {
            var student = this.usersRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return student;
        }
    }
}
