namespace SoftUniJobPlatform.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Web.ViewModels.Jobs;
    using SoftUniJobPlatform.Web.ViewModels.Student;
    using SoftUniJobPlatform.Web.ViewModels.StudentJob;

    public class JobsController : Controller
    {
        private readonly IJobsService jobsService;
        private readonly IApplicationUsersService userService;

        public JobsController(IJobsService jobsService, IApplicationUsersService userService)
        {
            this.jobsService = jobsService;
            this.userService = userService;
        }

        public IActionResult Index(int? pageNumber, string searchString = null)
        {

            this.ViewData["CurrentFilter"] = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                var viewModel = new AllJobsViewModel
                {
                    Jobs = this.jobsService.GetAll<JobsViewModel>().Where(x
                        => x.Description.ToLower().Contains(searchString.ToLower())),
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

        public IActionResult Candidates(int id)
        {



            var viewModel = this.jobsService.GetJobById<CandidateJobViewModel>(id);


            return this.View(viewModel);
        }
    }
}
