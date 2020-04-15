namespace SoftUniJobPlatform.Web.ViewModels.Student
{
    using System.Collections.Generic;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class StudentJobViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public IEnumerable<StudentJob> StudentJobs { get; set; }
    }
}
