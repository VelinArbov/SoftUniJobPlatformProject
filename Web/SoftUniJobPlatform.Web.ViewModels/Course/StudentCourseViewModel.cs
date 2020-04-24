namespace SoftUniJobPlatform.Web.ViewModels.Course
{
    using System.Collections.Generic;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class StudentCourseViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        //public IEnumerable<Course> Courses { get; set; }
    }
}