namespace SoftUniJobPlatform.Data.Models
{
   public class StudentJob
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int JobId { get; set; }

        public Job Job { get; set; }
    }
}
