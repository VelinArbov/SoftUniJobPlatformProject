namespace SoftUniJobPlatform.Web.ViewModels.Skills
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class SkillViewModel : IMapFrom<Skill>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UsersSkill> UsersSkills { get; set; }

        public bool Check { get; set; }
    }
}
