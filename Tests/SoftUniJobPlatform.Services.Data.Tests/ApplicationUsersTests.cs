namespace SoftUniJobPlatform.Services.Data.Tests
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using SoftUniJobPlatform.Data;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Data.Models.Enum;
    using SoftUniJobPlatform.Data.Repositories;
    using SoftUniJobPlatform.Services.Mapping;
    using Xunit;

    public class ApplicationUsersTests
    {
        public class MyTest : IMapFrom<ApplicationUser>
        {
            public string Email { get; set; }
        }

        [Fact]
        public void TestAllPositiveUsers()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var user = new ApplicationUser
            {
                Description = "test",
                Type = UserType.Employer,
            };
            repository.AddAsync(user).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);

            Assert.Single(usersService.GetAll<CompaniesServiceTests.MyTest>());
            repository.Delete(user);
            repository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        [Fact]
        public void TestNoUsers()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);
            Assert.Throws<ArgumentNullException>(() => usersService.GetAll<CompaniesServiceTests.MyTest>());
        }

        [Fact]
        public void TestsTakeAndDeleteUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var user = new ApplicationUser
            {
                Description = "test",
                Type = UserType.Employer,
            };
            repository.AddAsync(user).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);
            usersService.DeleteAsync(user.Id).GetAwaiter().GetResult();
            Assert.Empty(repository.All());
            repository.Delete(user);
            repository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        [Fact]
        public void TestsTakeAndDeleteUserFakeId()
        {
            string fakeId = "asdasgasdas";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var user = new ApplicationUser
            {
                Description = "test",
                Type = UserType.Employer,
            };
            repository.AddAsync(user).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);

            Assert.Throws<ArgumentNullException>(() => usersService.DeleteAsync(fakeId).GetAwaiter().GetResult());

        }

        [Fact]
        public void TestAllPositiveStudents()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var user = new ApplicationUser
            {
                Description = "test",
                Type = UserType.Student,
            };
            repository.AddAsync(user).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);

            Assert.Single(usersService.GetAll<CompaniesServiceTests.MyTest>());
            repository.Delete(user);
            repository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        [Fact]
        public void TestAllPositiveStudentsWithoutAdmin()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var user = new ApplicationUser
            {
                Description = "test",
                Type = UserType.Student,
            };
            var user1 = new ApplicationUser
            {
                Description = "test",
                Type = UserType.Student,
                IsAdmin = true,
            };
            repository.AddAsync(user).GetAwaiter().GetResult();
            repository.AddAsync(user1).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);

            Assert.Single(usersService.GetAllStudents<MyTest>());
            repository.Delete(user);
            repository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        [Fact]
        public void TestNoStudents()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);
            Assert.Empty(usersService.GetAllStudents<MyTest>());
        }

        [Fact]
        public void TestAddJobCorrectId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var user = new ApplicationUser
            {
                Description = "test",
                Type = UserType.Student,
            };
            var job = new Job
            {
                Title = "test",
            };
            jobRepository.AddAsync(job).GetAwaiter().GetResult();
            jobRepository.SaveChangesAsync().GetAwaiter().GetResult();
            repository.AddAsync(user).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);
            usersService.AddJobAsync(job.Id, user.Id).GetAwaiter().GetResult();
            Assert.Single(studentEfRepository.All());
        }

        [Fact]
        public void TestAddJobInCorrectIs()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var user = new ApplicationUser
            {
                Description = "test",
                Type = UserType.Student,
            };
            var job = new Job
            {
                Title = "test",
            };
            jobRepository.AddAsync(job).GetAwaiter().GetResult();
            jobRepository.SaveChangesAsync().GetAwaiter().GetResult();
            repository.AddAsync(user).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);
            Assert.Throws<ArgumentNullException>(() => usersService.AddJobAsync(2352342, user.Id).GetAwaiter().GetResult());
        }

        [Fact]
        public void TestAddJobInCorrectUserId()
        {
            string fakeId = "23423423";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var user = new ApplicationUser
            {
                Description = "test",
                Type = UserType.Student,
            };
            var job = new Job
            {
                Title = "test",
            };
            jobRepository.AddAsync(job).GetAwaiter().GetResult();
            jobRepository.SaveChangesAsync().GetAwaiter().GetResult();
            repository.AddAsync(user).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);
            Assert.Throws<ArgumentNullException>(() => usersService.AddJobAsync(job.Id, fakeId).GetAwaiter().GetResult());
        }

        [Fact]
        public void TestAddExistUserinJob()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var user = new ApplicationUser
            {
                Description = "test",
                Type = UserType.Student,
            };
            var job = new Job
            {
                Title = "test",
            };
            jobRepository.AddAsync(job).GetAwaiter().GetResult();
            jobRepository.SaveChangesAsync().GetAwaiter().GetResult();
            repository.AddAsync(user).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);
            usersService.AddJobAsync(job.Id, user.Id).GetAwaiter().GetResult();
            Assert.Throws<ArgumentNullException>(() => usersService.AddJobAsync(job.Id, user.Id).GetAwaiter().GetResult());
        }
    }
}
