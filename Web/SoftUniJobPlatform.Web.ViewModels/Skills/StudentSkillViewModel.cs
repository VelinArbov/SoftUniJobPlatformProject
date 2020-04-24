namespace SoftUniJobPlatform.Web.ViewModels.Skills
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class StudentSkillViewModel : IMapFrom<ApplicationUser>
    {
        public virtual ICollection<UsersSkill> UsersSkills { get; set; }
    }
}
