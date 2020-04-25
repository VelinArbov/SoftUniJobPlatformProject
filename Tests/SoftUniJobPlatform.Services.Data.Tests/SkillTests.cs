
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoftUniJobPlatform.Data;
using SoftUniJobPlatform.Data.Models;
using SoftUniJobPlatform.Data.Repositories;
using SoftUniJobPlatform.Services.Mapping;
using Xunit;

namespace SoftUniJobPlatform.Services.Data.Tests
{
    public class SkillTests
    {
        public class MyTest : IMapFrom<Skill>
        {
            public string Name { get; set; }

            public string Content { get; set; }

        }

        [Fact]
        public void TestGetSkillById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Skill>(new ApplicationDbContext(options.Options));
            var userSkillRepository = new EfRepository<UsersSkill>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Skill() { Name = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var skillsService = new SkillsService(repository, userSkillRepository);
            AutoMapperConfig.RegisterMappings(typeof(CategoryServicesTests.MyTestPost).Assembly);
            var post = skillsService.GetById(1);

            Assert.Equal("test", post.Name);
        }


        [Fact]
        public void DeleteSkillByFakeId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var userSkillRepository =
                new EfRepository<UsersSkill>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var skillRepository =
                new EfDeletableEntityRepository<Skill>(new ApplicationDbContext(options.Options));
            var skillService = new SkillsService(skillRepository, userSkillRepository);
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository, jobRepository, skillService, userSkillRepository, skillRepository);
            var user = new ApplicationUser
            {
                FullName = "Gosho"
            };

            var skill = new Skill
            {
                Name = "C#",
            };

            var userSkill = new UsersSkill
            {
                SkillId = skill.Id,
                ApplicationUserId = user.Id,

            };
            repository.AddAsync(user).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            skillRepository.AddAsync(skill).GetAwaiter().GetResult();
            skillRepository.SaveChangesAsync().GetAwaiter().GetResult();
            userSkillRepository.AddAsync(userSkill).GetAwaiter().GetResult();
            userSkillRepository.SaveChangesAsync();
            userSkillRepository.Delete(userSkill);
            skillService.DeleteAsync(skill.Id, user.Id).GetAwaiter().GetResult();
            Assert.Equal(1,userSkillRepository.All().Count());


        }

        [Fact]
        public void DeleteSkill()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var studentEfRepository =
                new EfRepository<StudentJob>(new ApplicationDbContext(options.Options));
            var userSkillRepository =
                new EfRepository<UsersSkill>(new ApplicationDbContext(options.Options));
            var jobRepository =
                new EfDeletableEntityRepository<Job>(new ApplicationDbContext(options.Options));
            var jobService = new JobsService(jobRepository, studentEfRepository);
            var skillRepository =
                new EfDeletableEntityRepository<Skill>(new ApplicationDbContext(options.Options));
            var skillService = new SkillsService(skillRepository, userSkillRepository);
            var usersService = new ApplicationUsersService(repository, jobService, studentEfRepository, jobRepository, skillService, userSkillRepository, skillRepository);
            var user = new ApplicationUser
            {
                FullName = "Gosho"
            };

            var skill = new Skill
            {
                Name = "C#",
            };

            var userSkill = new UsersSkill
            {
                SkillId = skill.Id,
                ApplicationUserId = user.Id,

            };
            repository.AddAsync(user).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            skillRepository.AddAsync(skill).GetAwaiter().GetResult();
            skillRepository.SaveChangesAsync().GetAwaiter().GetResult();
            userSkillRepository.AddAsync(userSkill).GetAwaiter().GetResult();
            userSkillRepository.SaveChangesAsync();
            userSkillRepository.Delete(userSkill);
            skillService.DeleteAsync(skill.Id, user.Id).GetAwaiter().GetResult();
            Assert.Equal(1, skillRepository.All().Count());


        }

        [Fact]
        public void TestGetAllCategories()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Skill>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Skill { Name = "title", Content = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var userSkillRepository =
                new EfRepository<UsersSkill>(new ApplicationDbContext(options.Options));
            var skillsService = new SkillsService(repository, userSkillRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTest).Assembly);
            var post = skillsService.GetAll<MyTest>(1);

            Assert.Single(skillsService.GetAll<MyTest>());
        }
    }
}
