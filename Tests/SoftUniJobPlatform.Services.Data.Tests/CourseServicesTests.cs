using System.Threading.Tasks;
using SoftUniJobPlatform.Data.Models.Enum;

namespace SoftUniJobPlatform.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using SoftUniJobPlatform.Data;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Data.Repositories;
    using SoftUniJobPlatform.Services.Mapping;
    using Xunit;

    public class CourseServicesTests
    {
        public class MyTest : IMapFrom<Course>
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public int CategoryId { get; set; }

            public Category Category { get; set; }

        }

        [Fact]
        public void GEtAllMappedCourse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Course() { Title = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var userRepository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var coursesService= new CoursesService(repository,userRepository);
            AutoMapperConfig.RegisterMappings(typeof(CategoryServicesTests.MyTestPost).Assembly);
            var post = coursesService.GetAll<MyTest>();

            Assert.Equal(1, repository.All().Count());

        }

        [Fact]
        public void GEtAllMappedNoCourses()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            var userRepository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var coursesService = new CoursesService(repository, userRepository);
            AutoMapperConfig.RegisterMappings(typeof(CategoryServicesTests.MyTestPost).Assembly);
            var post = coursesService.GetAll<MyTest>();
            Assert.Equal(0, repository.All().Count());
        }

        //[Fact]
        //public void GEtAllMappedCourseByUserId()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    var repository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
        //    var userRepository =
        //        new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
        //    repository.AddAsync(new Course() { Title = "test", ApplicationUserId = "asd"}).GetAwaiter().GetResult();
        //    repository.SaveChangesAsync().GetAwaiter().GetResult();
        //    var coursesService = new CoursesService(repository, userRepository);
        //    AutoMapperConfig.RegisterMappings(typeof(MyTest).Assembly);
        //    var course = coursesService.GetAllByUserId<MyTest>("asd");
        //    Assert.Equal(1,course.Count());

        //}

        [Fact]
        public void GEtCourseByUId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            var userRepository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Course() { Title = "test", ApplicationUserId = "asd" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var coursesService = new CoursesService(repository, userRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTest).Assembly);
            var course = coursesService.GetById(1);
            Assert.Equal("test",course.Title);

        }

        [Fact]
        public void DeleteCourseByUId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            var userRepository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Course() { Title = "test", ApplicationUserId = "asd" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var coursesService = new CoursesService(repository, userRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTest).Assembly);
            var course = coursesService.DeleteAsync(1);
            Assert.Empty(repository.All());

        }


        [Fact]
        public async Task AddCourseTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            var userRepository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var user = new ApplicationUser
            {
                FullName = "Test",
            };

            var course = new Course
            {
                Title = "test",
            };
            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            await repository.AddAsync(course);
            await repository.SaveChangesAsync();
            var coursesService = new CoursesService(repository, userRepository);
            coursesService.AddCourseAsync(course.Id, user.Id, 1, 1);
            Assert.Single(user.Courses);
        }

        [Fact]
        public async Task CreateCourseTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            var userRepository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var categoryRepository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            var category = new Category
            {
                Name = "test",
            };

            var user = new ApplicationUser
            {
                FullName = "Test",
            };

            var course = new Course
            {
                Id = 6,
                Title = "test",
            };
            await categoryRepository.AddAsync(category);
            await categoryRepository.SaveChangesAsync();
            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            await repository.AddAsync(course);
            await repository.SaveChangesAsync();
            var coursesService = new CoursesService(repository, userRepository);

            Assert.Throws<ArgumentException>(() =>
                coursesService.Create(user.Id, "title", "des", category.Id, "image", "Finished").GetAwaiter()
                    .GetResult());
        }

        [Fact]
        public async Task AddExistCourseTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            var userRepository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var user = new ApplicationUser
            {
                FullName = "Test",
            };

            var course = new Course
            {
                Title = "test",
            };
            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            await repository.AddAsync(course);
            await repository.SaveChangesAsync();
            var coursesService = new CoursesService(repository, userRepository);
            coursesService.AddCourseAsync(course.Id, user.Id, 1, 1);
            coursesService.AddCourseAsync(course.Id, user.Id, 1, 1);
            Assert.Single(user.Courses);
        }

        [Fact]
        public async Task GetCourseByFakeIdTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            var userRepository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Course() { Title = "test", ApplicationUserId = "asd" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var coursesService = new CoursesService(repository, userRepository);
            AutoMapperConfig.RegisterMappings(typeof(MyTest).Assembly);
            Assert.Throws<Exception>(() => coursesService.GetById<MyTest>(1));
        }
    }
}
