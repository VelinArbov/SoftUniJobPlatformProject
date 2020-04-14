﻿using System.Linq;

namespace SoftUniJobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoftUniJobPlatform.Data.Models;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IQueryable<Category> GetCategories();

        T GetByName<T>(string name);

        T GetById<T>(int id);

         Category GetById(int id);

        Task CreateAsync(string title, string description, string imageUrl);

        Task DeleteAsync(int id);

        Task EditAsync(int id, string title, string description, string imageUrl);
    }
}
