namespace SoftUniJobPlatform.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoftUniJobPlatform.Data.Common.Models;

    public class Student : BaseDeletableModel<string>
    {
        public Student()
        {
            this.Id = Guid.NewGuid().ToString();
            //this.Courses = new HashSet<StudentCourse>();
            //this.StudentJobs = new HashSet<StudentJob>();
        }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int StudentNumber { get; set; }

        public string Location { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ImageUrl { get; set; }

        public GenderType Gender { get; set; }

        public string PhoneNumber { get; set; }

        public double Raiting { get; set; }

        //public virtual ICollection<StudentCourse> Courses { get; set; }

        //public virtual ICollection<StudentJob> StudentJobs { get; set; }

    }
}
