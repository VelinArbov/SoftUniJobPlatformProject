
namespace SoftUniJobPlatform.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Web.ViewModels.Companies;

    public class StudentController : Controller
    {
        private readonly IApplicationUsersService usersService;

        public StudentController(IApplicationUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Details(string id)
        {
            var viewModel = this.usersService.GetStudentById<CompanyViewModel>(id);
            return this.View(viewModel);
        }
    }
}
