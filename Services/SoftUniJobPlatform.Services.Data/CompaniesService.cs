using System.Linq;
using SoftUniJobPlatform.Data.Common.Repositories;
using SoftUniJobPlatform.Data.Models;
using SoftUniJobPlatform.Data.Models.Enum;
using SoftUniJobPlatform.Services.Mapping;

namespace SoftUniJobPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CompaniesService : ICompaniesService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> companiesRepository;

        public CompaniesService(IDeletableEntityRepository<ApplicationUser> companiesRepository)
        {
            this.companiesRepository = companiesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query =
                this.companiesRepository.All().OrderBy(x => x.Type == UserType.Employer);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
