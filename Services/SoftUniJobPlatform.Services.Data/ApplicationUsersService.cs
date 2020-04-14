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
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJobsService jobsService;

        public ApplicationUsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            UserManager<ApplicationUser> userManager,
            IJobsService jobsService)
        {
            this.usersRepository = usersRepository;
            this.userManager = userManager;
            this.jobsService = jobsService;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query =
                this.usersRepository.All()
                    .OrderByDescending(x => x.CreatedOn);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task DeleteAsync(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

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
            var job = this.jobsService.GetJobById<Job>(jobId);
            var user = this.userManager.Users.FirstOrDefault(x => x.Id == userId);

            var studentJob = new StudentJob
            {
                ApplicationUser = user,
                Job = job,
            };
            await this.usersRepository.SaveChangesAsync();

        }
    }
}
