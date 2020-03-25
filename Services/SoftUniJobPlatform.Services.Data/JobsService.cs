

using System;
using System.Collections.Generic;
using System.Linq;
using SoftUniJobPlatform.Data.Common.Repositories;
using SoftUniJobPlatform.Data.Models;

namespace SoftUniJobPlatform.Services.Data
{
    public class JobsService : IJobsService
    {
        private readonly IDeletableEntityRepository<Job> jobRepository;

        public JobsService(IDeletableEntityRepository<Job> jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        public int GetAll()
        {
            return this.jobRepository.All().Count();
        }
    }
}
