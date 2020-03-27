namespace SoftUniJobPlatform.Data.Models
{
    public class UsersSkill
    {
        public int SkillId { get; set; }

        public Skill Skill { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
