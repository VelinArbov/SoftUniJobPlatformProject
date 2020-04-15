using System.Linq;

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

        public IActionResult Index(string searchString = null)
        {

            ViewData["CurrentFilter"] = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                var viewModel = new AllJobsViewModel
                {
                    Jobs = this.jobsService.GetAll<JobsViewModel>().Where(x
                        => x.Description.ToLower().Contains(searchString.ToLower()) ||
                           x.Title.ToLower().Contains(searchString.ToLower()) ||
                           x.CompanyName.ToLower().Contains(searchString.ToLower())),
                };

                return this.View(viewModel);
            }

            var viewModel1 = new AllJobsViewModel
            {
                Jobs = this.jobsService.GetAll<JobsViewModel>(),
            };

            return this.View(viewModel1);
        }

        public IActionResult Details(int id)
        {


            var viewModel = this.jobsService.GetJobById<JobsViewModel>(id);
            return this.View(viewModel);
        }

    }
}

