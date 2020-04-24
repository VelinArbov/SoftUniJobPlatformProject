﻿namespace SoftUniJobPlatform.Web.Areas.Employer.Controllers
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Hangfire;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Common;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Services.Mapping;
    using SoftUniJobPlatform.Web.Areas.Administration.Controllers;
    using SoftUniJobPlatform.Web.Areas.Employer.Controllers;
    using SoftUniJobPlatform.Web.ViewModels.Administration.Dashboard;
    using SoftUniJobPlatform.Web.ViewModels.Jobs;
    using SoftUniJobPlatform.Web.ViewModels.Student;

    public class DashboardController : EmployerController
    {
        private readonly IJobsService jobsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;
        private readonly IApplicationUsersService usersService;
        private readonly IEmailSender emailSender;

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
            this.emailSender = emailSender;
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
        public async Task<IActionResult> Create(JobsInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var jobsId = await this.jobsService.CreateJob(user.Id, input.Title, input.Description, input.Position, input.CategoryId, input.Level, input.Location, input.Salary, input.Engagement);

            var students = this.usersService.GetAll<StudentDetailsViewModel>();
            //await this.emailSender.SendEmailAsync("arbov.v@gmail.com", "VelinArbov", "softunijobs@abv.bg", "Test", "Test");

            BackgroundJob.Schedule(
                () => this.jobsService.DeleteAsync(jobsId),
                TimeSpan.FromDays(14));

            this.TempData["InfoMessage"] = "New job created!";
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

        public async Task<IActionResult> Delete(int id)
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
