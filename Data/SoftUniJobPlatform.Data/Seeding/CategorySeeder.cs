namespace SoftUniJobPlatform.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SoftUniJobPlatform.Data.Models;

    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category() { Title = ".NET", Description = "Although .NET and Visual Studio are fairly intuitive, there’s still a lot to learn about the framework. Whether you need a refresher on C#, or specific functions like parallel programming, design patterns, or dependency injection, Udemy has a course for you.", ImageUrl = "https://i.udemycdn.com/course/240x135/1148688_4313_3.jpg" });
        }
    }
}
