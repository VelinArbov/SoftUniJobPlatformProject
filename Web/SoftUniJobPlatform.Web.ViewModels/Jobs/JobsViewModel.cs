namespace SoftUniJobPlatform.Web.ViewModels.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class JobsViewModel : IMapFrom<Job>
    {
        public int Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string CompanyLogo => this.ApplicationUser.ImageUrl;

        public string Title { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string Engagement { get; set; }

        public string Level { get; set; }

        public int Salary { get; set; }

        public string Position { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public string ViewSalary => this.Salary == 0 ? "--" : this.Salary.ToString();

    }
}
