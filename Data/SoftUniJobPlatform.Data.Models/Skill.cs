namespace SoftUniJobPlatform.Data.Models
{
    using System.Collections.Generic;

    using SoftUniJobPlatform.Data.Common.Models;

    public class Skill : BaseDeletableModel<int>
    {
        public Skill()
        {
            this.UsersSkills = new HashSet<UsersSkill>();
        }

        public string Name { get; set; }

        public string Content { get; set; }

        public virtual ICollection<UsersSkill> UsersSkills { get; set; }

    }
}
