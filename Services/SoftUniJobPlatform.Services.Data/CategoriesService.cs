namespace SoftUniJobPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private const string InvalidCategoryIdErrorMessage = "Категория с  ID: {0} не същесвува.";
        private const string InvalidCategoryTitleErrorMessage = "Категория с име {0} не същесвува.";
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CategoriesService(
            IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? take = null, int skip = 0)
        {
            IQueryable<Category> query =
                this.categoriesRepository.All().OrderByDescending(x => x.CreatedOn).Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public IQueryable<Category> GetCategories()
        {
            var category = this.categoriesRepository.All();

            return category;
        }

        public T GetByName<T>(string name)
        {
            var category = this.categoriesRepository.All()
                .Where(x => x.Name.Replace(" ", "-") == name.Replace(" ", "-"))
                .To<T>().FirstOrDefault();

            if (category == null)
            {
                throw new Exception(
                    string.Format(InvalidCategoryIdErrorMessage, name));
            }

            return category;
        }

        public T GetById<T>(int id)
        {
            var category = this.categoriesRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            if (category == null)
            {
                throw new Exception(
                    string.Format(InvalidCategoryIdErrorMessage, id));
            }

            return category;
        }

        public Category GetById(int id)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                throw new Exception(
                    string.Format(InvalidCategoryIdErrorMessage, id));
            }

            return category;
        }

        public async Task<int> CreateAsync(string title, string description, string imageUrl)
        {
            var category = this.categoriesRepository.AddAsync(new Category
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl ?? "https://arbikas.com/pub/media/brands/asi.jpg",
            });

            var result = await this.categoriesRepository.SaveChangesAsync();
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                throw new Exception(
                    string.Format(InvalidCategoryIdErrorMessage, id));
            }

            this.categoriesRepository.Delete(category);

            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string title, string description, string imageUrl)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == id);
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description)
                                                 || string.IsNullOrWhiteSpace(imageUrl))
            {
                category.Title = title == null ? category.Title : title;
                category.Description = description == null ? category.Description : description;
                category.ImageUrl = imageUrl == null ? category.ImageUrl : imageUrl;
            }
            else if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description)
                                                         || string.IsNullOrEmpty(imageUrl))
            {
                category.Title = title == null ? category.Title : title;
                category.Description = description == null ? category.Description : description;
                category.ImageUrl = imageUrl == null ? category.ImageUrl : imageUrl;
            }
            else
            {
                category.Title = title;
                category.ImageUrl = imageUrl;
                category.Description = description;
            }

            await this.categoriesRepository.SaveChangesAsync();
        }
    }
}
