namespace SoftUniJobPlatform.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Hangfire;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
    using SoftUniJobPlatform.Common;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Services.Messaging;
    using SoftUniJobPlatform.Web.ViewModels.Administration.Dashboard;
    using SoftUniJobPlatform.Web.ViewModels.Categories;
    using SoftUniJobPlatform.Web.ViewModels.Jobs;

    public class DashboardController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IJobsService jobsService;
        private readonly IApplicationUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IEmailSender emailSender;
        private readonly ICloudinaryService cloudinaryService;
        private readonly Cloudinary cloudinary;

        public DashboardController(
            ICategoriesService categoriesService,
            IJobsService jobsService,
            IApplicationUsersService usersService,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IEmailSender emailSender,
            ICloudinaryService cloudinaryService,
            Cloudinary cloudinary)
        {
            this.categoriesService = categoriesService;
            this.jobsService = jobsService;
            this.usersService = usersService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.emailSender = emailSender;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Index()
        {
            var viewModel = new AllCategoriesViewModel
            {
                Categories = this.categoriesService.GetAll<CategoryViewModel>(),
            };
            return this.View(viewModel);
        }

        // Users Control//
        public IActionResult Users(AllUsersViewModel model)
        {
            var viewModel = new AllUsersViewModel
            {
                Users = this.usersService.GetAll<UserViewModel>(),
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.usersService.DeleteAsync(id);

            return this.RedirectToAction("Index");
        }

        // Categories Control //
        public IActionResult CreateCategory()
        {
            return this.View("Category/CreateCategory");
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryViewModel model)
        {
            var imageUrl = await this.cloudinaryService.UploadFormFileAsync(model.Image);
            await this.categoriesService.CreateAsync(model.Title, model.Description, imageUrl);
            await this.emailSender.SendEmailAsync("arbov.v@gmail.com", "VelinArbov", "softunijobs@abv.bg", "Test", "Test");
            return this.RedirectToAction("Index");
        }

        public IActionResult EditCategory(int id)
        {
            var viewModel = this.categoriesService.GetById<CategoryViewModel>(id);

            return this.View("Category/EditCategory", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryViewModel model)
        {
            await this.categoriesService.EditAsync(model.Id, model.Title, model.Description, model.ImageUrl);

            return this.Redirect("/Administration/Dashboard");
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await this.categoriesService.DeleteAsync(id);
            return this.Redirect("/Administration/Dashboard");
        }

        // Jobs Control //
        public IActionResult EditJob(int id)
        {
            var viewModel = this.jobsService.GetJobById<JobsViewModel>(id);

            return this.View("Job/EditJob", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditJob(JobsViewModel model)
        {
            await this.jobsService.EditAsync(model.Id, model.Level, model.Location, model.Level, model.Engagement,
                model.Salary);

            return this.Redirect("/Administration/Dashboard");
        }

        public async Task<IActionResult> DeleteJob(int id)
        {
            await this.jobsService.DeleteAsync(id);
            return this.Redirect("/Jobs");
        }

        // Moderator Control //
        public async Task<IActionResult> AddModerator(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var roles = await this.userManager.GetRolesAsync(user);

            await this.userManager.AddToRoleAsync(user, GlobalConstants.ModeratorRoleName);

            return this.Redirect("/Administration/Dashboard/Users");

        }

        public async Task<IActionResult> DeleteModerator(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var roles = await this.userManager.GetRolesAsync(user);

            await this.userManager.RemoveFromRoleAsync(user, GlobalConstants.ModeratorRoleName);

            return this.Redirect("/Administration/Dashboard/Users");

        }


        public async Task<string> UploadImg(Cloudinary cloudinary, IFormFile file)
        {
            byte[] destinationData;

            ImageUploadResult finalResult = null;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationData = memoryStream.ToArray();
            }



            using (var memoryStream = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, memoryStream),
                };

                 finalResult = await this.cloudinary.UploadAsync(uploadParams);
                
            }

            return finalResult.SecureUri.AbsoluteUri;
        }
    }
}
