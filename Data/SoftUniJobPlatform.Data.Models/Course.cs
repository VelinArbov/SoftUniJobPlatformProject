using System;
using System.Collections.Generic;
using System.Text;
using SoftUniJobPlatform.Data.Common.Models;

namespace SoftUniJobPlatform.Data.Models
{
    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.StudentCourses = new HashSet<StudentCourse>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
