using System.Threading.Tasks;

namespace SoftUniJobPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoftUniJobPlatform.Data.Models;

    public interface ISkillsService
    {
        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        Skill GetById(int id);

        Task DeleteAsync(int id, string userId);
    }
}
