using System;
using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using SoftUniJobPlatform.Web.ViewModels.Administration.Dashboard;

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
        private readonly IApplicationUsersService usersService;

        public DashboardController(
            IJobsService jobsService,
            UserManager<ApplicationUser> userManager,
            ICategoriesService categoriesService,
            IApplicationUsersService usersService,
            IEmailSender emailSender)
        {
            this.jobsService = jobsService;
            this.userManager = userManager;
            this.categoriesService = categoriesService;
            this.usersService = usersService;
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

            BackgroundJob.Schedule(
                () => this.jobsService.DeleteAsync(jobsId),
                TimeSpan.FromDays(14));

            this.TempData["InfoMessage"] = "New jobs created!";
            return this.Redirect("/");
        }

        public IActionResult EditJob(int id)
        {
            var viewModel = this.jobsService.GetJobById<JobsViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditJob(JobsViewModel model)
        {
            await this.jobsService.EditAsync(model.Id, model.Level, model.Location, model.Level, model.Engagement,
                model.Salary);

            return this.Redirect("/Employer/Dashboard/");
        }

        public async Task<IActionResult> DeleteJobAsync(int id)
        {
            await this.jobsService.DeleteAsync(id);
            return this.Redirect("/Jobs");
        }

        public IActionResult AllStudents()
        {
            var viewModel = new AllUsersViewModel
            {
                Users = this.usersService.GetAllStudents<UserViewModel>(),
            };
            return this.View(viewModel);
        }
    }
}
