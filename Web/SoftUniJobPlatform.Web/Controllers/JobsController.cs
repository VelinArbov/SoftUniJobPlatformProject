using SoftUniJobPlatform.Services.Data;
using SoftUniJobPlatform.Web.ViewModels.Jobs;

namespace SoftUniJobPlatform.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class JobsController : Controller
    {
        private readonly IJobsService jobsService;

        public JobsController(IJobsService jobsService)
        {
            this.jobsService = jobsService;
        }

        public IActionResult GetAll() {
            var jobs = this.jobsService.GetAll();

            return this.View(jobs);
        }
    }
}
