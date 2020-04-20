namespace SoftUniJobPlatform.Web.ViewModels.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class AllJobsViewModel : PageModel
    {
        public IEnumerable<JobsViewModel> Jobs { get; set; }

        public int CurrentPage { get; set; }

    }
}
