namespace SoftUniJobPlatform.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoftUniJobPlatform.Data.Common.Models;
    using SoftUniJobPlatform.Data.Models.Enum;

    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.StudentCourses = new HashSet<StudentCourse>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string ImageUrl { get; set; }

        public CourseProgressType CourseProgress { get; set; }

        public double Rate { get; set; }

        public int Credit { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
