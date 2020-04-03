namespace SoftUniJobPlatform.Web.ViewModels.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllJobsViewModel
    {
       public IEnumerable<JobsViewModel> Jobs { get; set; }
    }
}
