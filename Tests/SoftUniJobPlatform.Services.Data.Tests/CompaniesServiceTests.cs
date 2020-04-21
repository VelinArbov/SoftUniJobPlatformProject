namespace SoftUniJobPlatform.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;
    using SoftUniJobPlatform.Data;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Data.Models.Enum;
    using SoftUniJobPlatform.Data.Repositories;
    using SoftUniJobPlatform.Services.Mapping;
    using Xunit;

    public class CompaniesServiceTests
    {
        public class MyTest : IMapFrom<ApplicationUser>
        {
            public string Email { get; set; }

        }

        [Fact]
        public void TestAllPositiveCompanies()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            var user = new ApplicationUser
            {
                Description = "test",
                Type = UserType.Employer,
            };
            repository.AddAsync(user).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var company = new CompaniesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);
            var job = company.GetAll<CompaniesServiceTests.MyTest>();

            Assert.Single(company.GetAll<CompaniesServiceTests.MyTest>());
            repository.Delete(user);
            repository.SaveChangesAsync().GetAwaiter().GetResult();

        }

        [Fact]
        public void TestAllNegativeCompanies()
        {
            var user = new ApplicationUser();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new ApplicationUser()
            {
                Description = "test",
                Type = UserType.Student,
            });

            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var company = new CompaniesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);
            Assert.Throws<ArgumentNullException>(() => company.GetAll<CompaniesServiceTests.MyTest>());
        }



        [Fact]
        public void GetCompanyWithId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var user = new ApplicationUser()
            {
                Email = "test",
                Description = "test",
                Type = UserType.Employer,
            };

            repository.AddAsync(user);
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var companyService = new CompaniesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);
            var company = companyService.GetCompanyAsync<MyTest>(user.Id);
            Assert.Equal("test", company.Email);
            
        }

        [Fact]
        public void GetCompanyWithFakeId()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new ApplicationUser()
            {
                Description = "test",
                Type = UserType.Employer,
            });

            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var company = new CompaniesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(CompaniesServiceTests.MyTest).Assembly);
            Assert.Throws<ArgumentNullException>(() => company.GetCompanyAsync<MyTest>("dasdasads"));
        }
    }
}
