using SoftUniJobPlatform.Web.ViewModels.Student;

namespace SoftUniJobPlatform.Web.Areas.Student.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Web.Areas.Administration.Controllers;
    using SoftUniJobPlatform.Web.ViewModels.Jobs;

    public class DashboardController : StudentController
    {
        private readonly IApplicationUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(
            IApplicationUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
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

    }
}
