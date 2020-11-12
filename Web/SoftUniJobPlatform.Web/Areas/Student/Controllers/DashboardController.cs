using System;

namespace SoftUniJobPlatform.Web.Areas.Student.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Web.Areas.Administration.Controllers;
    using SoftUniJobPlatform.Web.ViewModels.Companies;
    using SoftUniJobPlatform.Web.ViewModels.Course;
    using SoftUniJobPlatform.Web.ViewModels.Skills;
    using SoftUniJobPlatform.Web.ViewModels.Student;

    public class DashboardController : StudentController
    {
        private readonly IApplicationUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICoursesService coursesService;
        private readonly ISkillsService skillsService;
        private readonly ICloudinaryService cloudinary;

        public DashboardController(
            IApplicationUsersService usersService,
            UserManager<ApplicationUser> userManager,
            ICoursesService coursesService,
            ISkillsService skillsService,
            ICloudinaryService cloudinary)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.coursesService = coursesService;
            this.skillsService = skillsService;
            this.cloudinary = cloudinary;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = this.usersService.GetStudentById<StudentJobViewModel>(userId);
            return this.View(viewModel);
        }

        public async Task<IActionResult> ApplyJob(int id)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                await this.usersService.AddJobAsync(id, userId);
                this.TempData["InfoMessage"] = "Благодарим Ви че кандидствахте по тази обява.";
            }
            catch (Exception e)
            {
                this.TempData["ErrorMessage"] = e.Message;
                this.TempData["InfoMessage"] = null;
            }

            return this.Redirect("/Jobs");
        }



        [HttpPost]
        public IActionResult AddCourse(CourseViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.coursesService.AddCourseAsync(model.Id, userId, model.Rate, model.Credit);
            return this.Redirect("/");
        }

        public IActionResult AllSKills()
        {
            var viewModel = new AllSkillsViewModel
            {
                Skills = this.skillsService.GetAll<SkillViewModel>(),
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> AddSkill(int id)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                await this.usersService.AddSkillAsync(id, userId);
                this.TempData["InfoMessage"] = "Добавихте ново умение";

            }
            catch (Exception e)
            {
                this.TempData["ErrorMessage"] = e.Message;
            }

            return this.Redirect("/Student/Dashboard/MySkills");
        }

        public IActionResult MySkills()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = this.usersService.GetStudentById<StudentSkillViewModel>(userId);
            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.skillsService.DeleteAsync(id, userId);
            return this.Redirect("/Student/Dashboard/MySkills");
        }

        public IActionResult CreateCourse()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateCourse(CourseViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var imageUrl = this.cloudinary.UploadFormFileAsync(model.Image).GetAwaiter().GetResult();
            this.coursesService.Create(userId, model.Title, model.Description, model.CategoryId, imageUrl,model.CourseProgress);
            return this.RedirectToAction("Index");
        }

        public IActionResult MyCourses()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new AllCoursesViewModel
            {

                Courses = this.coursesService.GetAllByUserId<CourseViewModel>(userId),
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> DeleteC(int id)
        {
            await this.coursesService.DeleteAsync(id);
            return this.Redirect("/Student/Dashboard/MyCourses");
        }
    }
}
