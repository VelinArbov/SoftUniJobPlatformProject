namespace SoftUniJobPlatform.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Web.ViewModels.Jobs;

    public class JobsController : Controller
    {
        private readonly IJobsService jobsService;

        public JobsController(IJobsService jobsService)
        {
            this.jobsService = jobsService;
        }

        public IActionResult Index()
        {
            var viewModel = new AllJobsViewModel
            {
                Jobs = this.jobsService.GetAll<JobsViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.jobsService.GetJobById<JobsViewModel>(id);
            return this.View(viewModel);
        }

    }
}
