
namespace SoftUniJobPlatform.Data.Models
{
   public class StudentJob
    {
        public string StudentId { get; set; }

        public Student Student { get; set; }

        public int JobId { get; set; }

        public Job Job { get; set; }
    }
}
