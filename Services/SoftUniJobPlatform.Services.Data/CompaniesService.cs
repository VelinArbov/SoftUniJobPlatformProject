namespace SoftUniJobPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Data.Models.Enum;
    using SoftUniJobPlatform.Services.Mapping;

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
                this.companiesRepository.All()
                    .Where(x => x.Type == UserType.Employer)
                    .OrderByDescending(x => x.CreatedOn);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetCompanyAsync<T>(string id)
        {
            var job = this.companiesRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return job;
        }
    }
}
