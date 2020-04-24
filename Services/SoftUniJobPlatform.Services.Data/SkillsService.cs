using System.Threading.Tasks;

namespace SoftUniJobPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class SkillsService : ISkillsService
    {
        private readonly IDeletableEntityRepository<Skill> skillRepository;
        private readonly IRepository<UsersSkill> userSkillRepository;

        public SkillsService(IDeletableEntityRepository<Skill> skillRepository,
            IRepository<UsersSkill> userSkillRepository)
        {
            this.skillRepository = skillRepository;
            this.userSkillRepository = userSkillRepository;
        }

        public IEnumerable<T> GetAll<T>(int? take = null, int skip = 0)
        {
            IQueryable<Skill> query =
                this.skillRepository.All().OrderByDescending(x => x.CreatedOn).Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public Skill GetById(int id)
        {
            return this.skillRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var skill = this.skillRepository.All().FirstOrDefault(x => x.Id == id);

            if (skill == null)
            {
                throw new ArgumentNullException();
            }

            var isExist = this.userSkillRepository.All().Any(x => x.SkillId == id);

            if (isExist)
            {
                var usersSkill = this.userSkillRepository.All().FirstOrDefault(x => x.SkillId == id && x.ApplicationUserId == userId);

                this.userSkillRepository.Delete(usersSkill);

                await this.userSkillRepository.SaveChangesAsync();
            }

        }
    }
}
