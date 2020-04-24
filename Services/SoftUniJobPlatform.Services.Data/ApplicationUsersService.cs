namespace SoftUniJobPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Data.Models.Enum;
    using SoftUniJobPlatform.Services.Mapping;

    public class ApplicationUsersService : IApplicationUsersService
    {
        private const string UserAlreadyApplyOffer = "Вече сте кандидатсвали по тази обява!";
        private const string NoUserWithId = "Няма юзър с това ИД {0}";
        private const string UserAlreadyApplySkill = "Имате вече добавен този скил.";
        private const string NotCorrectInput = "Грешни данни";
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IJobsService jobsService;
        private readonly IRepository<StudentJob> studentJobRepository;
        private readonly IDeletableEntityRepository<Job> jobRepository;
        private readonly ISkillsService skillsService;
        private readonly IRepository<UsersSkill> usersSkillRepository;
        private readonly IDeletableEntityRepository<Skill> skillRepository;

        public ApplicationUsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IJobsService jobsService,
            IRepository<StudentJob> studentJobRepository,
            IDeletableEntityRepository<Job> jobRepository,
            ISkillsService skillsService,
            IRepository<UsersSkill> usersSkillRepository,
            IDeletableEntityRepository<Skill> skillRepository)

        {
            this.usersRepository = usersRepository;
            this.jobsService = jobsService;
            this.studentJobRepository = studentJobRepository;
            this.jobRepository = jobRepository;
            this.skillsService = skillsService;
            this.usersSkillRepository = usersSkillRepository;
            this.skillRepository = skillRepository;
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
            var user = this.usersRepository.All()
                .FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new Exception(string.Format(NoUserWithId, id));
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
                throw new Exception(NotCorrectInput);
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
                job.Candidates.Add(studentJob);
                user.StudentJobs.Add(studentJob);
                await this.jobRepository.SaveChangesAsync();
                await this.usersRepository.SaveChangesAsync();

            }
            else
            {
                throw new Exception(UserAlreadyApplyOffer);
            }
        }

        public async Task AddSkillAsync(int jobId, string userId)
        {
            var user = this.usersRepository.All()
                .FirstOrDefault(x => x.Id == userId);
            var skill = this.skillsService.GetById(jobId);
            if (user == null || skill == null)
            {
                throw new Exception(NotCorrectInput);
            }

            var exist = this.usersSkillRepository.All().FirstOrDefault(x => x.SkillId == jobId && x.ApplicationUserId == userId);
            if (exist == null)
            {
                var usersSkill = new UsersSkill
                {
                    ApplicationUserId = userId,
                    ApplicationUser = user,
                    SkillId = jobId,
                    Skill = skill,
                };
                skill.Check = true;
                skill.UsersSkills.Add(usersSkill);
                user.UsersSkills.Add(usersSkill);
                await this.skillRepository.SaveChangesAsync();
                await this.usersRepository.SaveChangesAsync();

            }
            else
            {
                throw new Exception(UserAlreadyApplySkill);
            }
        }

        public T GetStudentById<T>(string id)
        {
            var student = this.usersRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return student;
        }

        public ApplicationUser GetStudentById(string id)
        {
            return this.usersRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public int GetStudentsCount()
        {
            return this.usersRepository.All().Count(x => x.Type == UserType.Student && x.IsAdmin == false);
        }

        public int GetCompaniesCount()
        {
            return this.usersRepository.All().Count(x => x.Type == UserType.Employer);
        }
    }
}
