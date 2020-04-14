namespace SoftUniJobPlatform.Web.ViewModels.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class ApplyJobViewModel : IMapFrom<StudentJob>
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int JobId { get; set; }

        public Job Job { get; set; }
    }
}
