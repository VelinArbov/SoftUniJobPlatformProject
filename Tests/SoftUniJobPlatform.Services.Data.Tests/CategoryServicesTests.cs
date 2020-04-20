namespace SoftUniJobPlatform.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoftUniJobPlatform.Data;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Data.Repositories;
    using SoftUniJobPlatform.Services.Mapping;
    using Xunit;

    public class CategoryServicesTests
    {
        public class MyTestPost : IMapFrom<Category>
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public string Name { get; set; }

            public string ImageUrl { get; set; }
        }

        [Fact]
        public void TestGetCategoryById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Category() {Title = "test"}).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var postService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var post = postService.GetById<MyTestPost>(1);

            Assert.Equal("test", post.Title);
        }

        [Fact]
        public void TestGetCategoryNoExistId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Category() { Title = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var postService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            int notReal = 615260206;

            Assert.Throws<ArgumentNullException>(() => postService.GetById<MyTestPost>(notReal));
        }

        [Fact]

        public void TestGetCategoryDescription()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Category() {Title = "title", Description = "test"}).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var postService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var post = postService.GetById<MyTestPost>(1);

            Assert.Equal("test", post.Description);
        }

        [Fact]
        public void TestGetAllCategories()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Category() { Title = "title", Description = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var post = categoryService.GetById<MyTestPost>(1);

            Assert.Single(categoryService.GetAll<MyTestPost>());
        }

        [Fact]
        public void TestGetAllWithNUllCategories()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);

            Assert.Throws<ArgumentNullException>(() => categoryService.GetAll<MyTestPost>());
        }

        [Fact]
        public void TestGetCategories()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Category() { Title = "title", Description = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var category = categoryService.GetCategories();

            Assert.Equal(1, category.Count());
        }

        [Fact]
        public void TestGetCategoriesIfNoCategories()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);

            Assert.Throws<ArgumentNullException>(() => categoryService.GetCategories());
        }


        [Fact]
        public void TestGetByNameNonExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Category() { Title = "title", Description = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            Assert.Throws<ArgumentNullException>(() => categoryService.GetByName<MyTestPost>("title"));
        }

        [Fact]
        public void TestGetByNameExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Category() { Title = "title", Description = "test", Name = "test"}).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var category = categoryService.GetByName<MyTestPost>("test");
            Assert.Equal("test",category.Name);
        }



        [Fact]
        public void TestGetByIdReturnCategory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Category() { Title = "title", Description = "test", Name = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var category = categoryService.GetById(1);
            Assert.Equal("test", category.Name);
        }


        [Fact]
        public void TestGetByIdReturnCategoryWithFakeId()
        {
            int fakeId = 2321616;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Category() { Title = "title", Description = "test", Name = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            Assert.Throws<ArgumentNullException>(() => categoryService.GetById(fakeId));
        }


        [Fact]
        public async Task TestCreateAsyncCategory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            var categoryService = new CategoriesService(repository);
            await categoryService.CreateAsync("test", "test", "test");
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            Assert.Equal(1,repository.All().Count());
        }


        [Theory]
        [InlineData(" "," "," ")]
        [InlineData("","    "," ")]
        [InlineData(null," "," ")]
        [InlineData(null,null," ")]
        [InlineData(null,null,null)]
        public async Task TestCreateAsyncCategoryNotValid(string title,string description,string imageUrl)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                 categoryService.CreateAsync(title, description, imageUrl));
        }

        [Fact]
        public async Task DeleteAsyncCategoryNotValid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            repository.AddAsync(new Category() { Title = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                categoryService.DeleteAsync(18));
        }


        [Fact]
        public async Task DeleteAsyncCategoryValid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            repository.AddAsync(new Category() { Title = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            await categoryService.DeleteAsync(1);
            Assert.Equal(0, repository.All().Count());

        }


        [Fact]
        public async Task EditAsyncCategoryValid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            repository.AddAsync(new Category() { Title = "test", Description = "test", ImageUrl = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var category = categoryService.GetById(1);
            await categoryService.EditAsync(1,"test1", "test1", "test1");
            Assert.Equal("test1",category.Title);

        }

        [Fact]
        public async Task EditAsyncCategoryNotValid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            var categoryService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            repository.AddAsync(new Category() { Title = "test", Description = "test", ImageUrl = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var category = categoryService.GetById(1);
            await categoryService.EditAsync(1, null, "asdasdas", "test1");
            Assert.Equal("test", category.Title);

        }
    }
}


