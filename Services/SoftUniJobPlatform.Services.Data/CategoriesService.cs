using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SoftUniJobPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CategoriesService(
            IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query =
                this.categoriesRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IQueryable<Category> GetCategories()
        {
            return this.categoriesRepository.All();
        }

        public T GetByName<T>(string name)
        {
            var category = this.categoriesRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();
            return category;
        }

        public T GetById<T>(int id)
        {
            var category = this.categoriesRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return category;
        }

        public Category GetById(int id)
        {
            return this.categoriesRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public async Task CreateAsync(string title, string description, string imageUrl)
        {
            var category = this.categoriesRepository.AddAsync(new Category
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
            });

            
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == id);

            this.categoriesRepository.Delete(category);

            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string title, string description, string imageUrl)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == id);

            category.Title = title == null ? category.Title : title;
            category.Description = description == null ? category.Description : description;
            category.ImageUrl = imageUrl == null ? category.ImageUrl : imageUrl;

            await this.categoriesRepository.SaveChangesAsync();
        }
    }
}
