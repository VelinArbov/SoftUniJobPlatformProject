namespace SoftUniJobPlatform.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using SoftUniJobPlatform.Data;
    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Data.Repositories;
    using SoftUniJobPlatform.Services.Mapping;
    using SoftUniJobPlatform.Web.ViewModels.Jobs;
    using Xunit;

    public class JobServicesTests
    {

        public class MyTestJob : IMapFrom<Job>
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public string Name { get; set; }

            public string ImageUrl { get; set; }

            public string Location { get; set; }
        }

        [Fact]
        public void TestGetAllJob()
        {
            ApplicationUser user = new ApplicationUser();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",

            });

            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(JobServicesTests.MyTestJob).Assembly);
            var job = jobService.GetAll<JobServicesTests.MyTestJob>();

            Assert.Single(jobService.GetAll<JobServicesTests.MyTestJob>());
        }

        [Fact]
        public void TestGetAllJobWithNullJobs()
        {
            ApplicationUser user = new ApplicationUser();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(JobServicesTests.MyTestJob).Assembly);

            Assert.Empty( jobService.GetAll<JobServicesTests.MyTestJob>());
        }


        [Fact]
        public async Task TestCreateAsyncMethodCorect()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(repository, studentRepo);
            await jobService.CreateJob(user.Id, "test", "test", "test", category.Id, "Junior", "Pleven",
                8000, "FullTime");
            AutoMapperConfig.RegisterMappings(typeof(JobServicesTests.MyTestJob).Assembly);
            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task TestCreateAsyncMethodINCorect()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(repository, studentRepo);
            await jobService.CreateJob(user.Id, null, null, "test", category.Id, "Junior", "Pleven",
                8000, "FullTime");
            AutoMapperConfig.RegisterMappings(typeof(JobServicesTests.MyTestJob).Assembly);
            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public void TestGetAuthorJobNoExistId()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var notReal = "sdaasdasg";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",

            });
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            Assert.Empty( jobService.GetById<MyTestJob>(notReal));
        }

        [Fact]
        public void TestGetAuthorJobExistId()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",

            });
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            var company = jobService.GetById<MyTestJob>(user.Id);
            Assert.Single(company);
        }

        [Fact]
        public void TestSearchJobs()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",

            });
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            var company = jobService.SearchJob("test");
            Assert.Equal("test", company.Where(x => x.ApplicationUserId == user.Id)
                .Select(x => x.Description)
                .FirstOrDefault());
        }

        [Fact]
        public void TestSearchJobsInvalidParameter()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",

            });
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            var company = jobService.SearchJob("dadasdaasdasd");
            Assert.Equal(0, company.Count());
        }

        [Fact]
        public void TestSearchJobsInvalidParameterOrNull()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",

            });
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            Assert.Empty(jobService.SearchJob(" "));
        }

        [Fact]
        public void TestGetBYIncorrectCategoryId()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",

            });
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);

            Assert.Throws<ArgumentNullException>(() => jobService.GetByCategoryId<MyTestJob>(182152));
        }

        [Fact]
        public void TestGetBYCategoryId()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",

            });
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            jobService.GetByCategoryId<MyTestJob>(category.Id);
            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public void TestGetJobById()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var job = new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",
            };
            repository.AddAsync(job);
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            var exaclyJob = jobService.GetJobById<MyTestJob>(job.Id);
            Assert.Equal("test", exaclyJob.Description);
        }

        [Fact]
        public void TestGetJobByIncorrectId()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var job = new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",
            };
            repository.AddAsync(job);
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            Assert.Throws<Exception>(() => jobService.GetJobById<MyTestJob>(18683));
        }

        [Fact]
        public void TestGetJobCorectId()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var job = new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",
            };
            repository.AddAsync(job);
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            var newJob = jobService.JobById(job.Id);
            Assert.Equal("test", newJob.Description);
        }

        [Fact]
        public async Task TestDeleteJObAsync()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var job = new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",
            };
            await repository.AddAsync(job);
            await repository.SaveChangesAsync();

            var studentJob = new StudentJob
            {
                Job = job,
                ApplicationUser = user,
            };

            await studentRepo.AddAsync(studentJob);

            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            await jobService.DeleteAsync(job.Id);

            Assert.Equal(0, repository.All().Count());
        }

        [Fact]
        public async Task DeleteIncorrectId()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var job = new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",
            };
            await repository.AddAsync(job);
            await repository.SaveChangesAsync();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            var newJob = jobService.JobById(job.Id);
            await Assert.ThrowsAsync<Exception>(() => jobService.DeleteAsync(18));
        }

        [Fact]
        public void TestGetCountByCategoryIncorrectId()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var job = new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",
            };
            repository.AddAsync(job);
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            var newJob = jobService.JobById(job.Id);
            Assert.Throws<Exception>(() => jobService.GetCountByCategoryId(18));
        }

        [Fact]
        public void TestGetCountByCategoryId()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var job = new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",
                Category = category,
            };
            repository.AddAsync(job);
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            var jobs = jobService.GetCountByCategoryId(category.Id);
            Assert.Equal(1, jobs);

        }

        [Fact]
        public async Task TestEditJobAsync()
        {
            ApplicationUser user = new ApplicationUser();
            Category category = new Category();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var studentRepo = new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var job = new Job
            {
                ApplicationUser = user,
                Salary = 900,
                Description = "test",
                Category = category,
            };
            await repository.AddAsync(job);
            await repository.SaveChangesAsync();
            var jobService = new JobsService(repository, studentRepo);
            AutoMapperConfig.RegisterMappings(typeof(MyTestJob).Assembly);
            await jobService.EditAsync(job.Id, "Junior", "Pleven", "Junior", "Remote", 80000);
            var newJob = jobService.GetJobById<MyTestJob>(job.Id);
            Assert.Equal("Pleven",newJob.Location);

        }

    }
}
