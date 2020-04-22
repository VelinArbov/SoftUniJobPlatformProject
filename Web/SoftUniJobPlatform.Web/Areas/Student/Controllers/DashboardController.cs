namespace SoftUniJobPlatform.Web.Areas.Student.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Web.Areas.Administration.Controllers;
    using SoftUniJobPlatform.Web.ViewModels.Course;
    using SoftUniJobPlatform.Web.ViewModels.Student;

    public class DashboardController : StudentController
    {
        private readonly IApplicationUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICoursesService coursesService;

        public DashboardController(
            IApplicationUsersService usersService,
            UserManager<ApplicationUser> userManager,
            ICoursesService coursesService)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.coursesService = coursesService;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = this.usersService.GetStudentById<StudentJobViewModel>(userId);
            return this.View(viewModel);
        }

        public async Task<IActionResult> ApplyJob(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.usersService.AddJobAsync(id, userId);
            return this.Redirect("/Jobs");
        }

        public IActionResult AllCourses()
        {
            var viewModel = new AllCoursesViewModel()
            {
                Courses = this.coursesService.GetAll<CourseViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult AddCourse(int id)
        {
            var viewmodel = this.coursesService.GetById<CourseViewModel>(id);
            return this.View(viewmodel);
        }

        [HttpPost]
        public IActionResult AddCourse(CourseViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewmodel = this.coursesService.AddCourseAsync(model.Id, userId, model.Rate, model.Credit);
            return this.Redirect("/");
        }

        public IActionResult MyCourses()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = this.usersService.GetStudentById<StudentCourseViewModel>(userId);
            return this.View(viewModel);
        }

    }
}
