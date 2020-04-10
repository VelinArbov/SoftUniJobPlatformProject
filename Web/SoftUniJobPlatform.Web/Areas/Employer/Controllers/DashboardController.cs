namespace SoftUniJobPlatform.Web.Areas.Employer.Controllers
{
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Services.Mapping;
    using SoftUniJobPlatform.Web.Areas.Administration.Controllers;
    using SoftUniJobPlatform.Web.Areas.Employer.Controllers;
    using SoftUniJobPlatform.Web.ViewModels.Jobs;

    public class DashboardController : EmployerController
    {
        private readonly IJobsService jobsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;

        public DashboardController(
            IJobsService jobsService,
            UserManager<ApplicationUser> userManager,
            ICategoriesService categoriesService)
        {
            this.jobsService = jobsService;
            this.userManager = userManager;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = new AllJobsViewModel
            {
                Jobs = this.jobsService.GetById<JobsViewModel>(user.Id),
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(JobsViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var jobsId = await this.jobsService.CreateJob(user.Id, input.Title, input.Description, input.Position, input.CategoryId, input.Level, input.Location, input.Salary, input.Engagement);
            this.TempData["InfoMessage"] = "New jobs created!";
            return this.Redirect("/");
        }
    }
}
