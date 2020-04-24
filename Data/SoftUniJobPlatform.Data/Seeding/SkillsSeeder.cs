namespace SoftUniJobPlatform.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SoftUniJobPlatform.Data.Models;

    public class SkillsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Skills.Any())
            {
                return;
            }

            var skills = new List<Skill>();

            var stringSKills = new List<string>
            {
                "Application Development",
                "Architecture",
                "Artificial Intelligence ",
                "Cloud Computing ",
                "HTML",
                "C++",
                "C# ",
                "PHP",
                "UX Design",
                "Python",
                "JavaScript",
                "Java",
                "Ruby",
                "IP Setup",
                "Wireless Modems/Routers",
                "Cloud Services",
                "PHP",
                "SQL:",
                "JavaScript",
                "Python",
                "C++",
                "Functionality",
                "Cyber Security",
                "Information Management",
                "Cloud Systems Administration",
            };

            foreach (var item in stringSKills)
            {
                var newSkill = new Skill
                {
                    Name = item,
                    CreatedOn = DateTime.UtcNow,
                };
                skills.Add(newSkill);
            }

            await dbContext.Skills.AddRangeAsync(skills);
        }
    }
}
