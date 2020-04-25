
using Microsoft.AspNetCore.Authorization;
using SoftUniJobPlatform.Common;

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

        [Authorize(Roles = GlobalConstants.EmployerRoleName + "," + GlobalConstants.ModeratorRoleName + "," + GlobalConstants.AdministratorRoleName)]
        public IActionResult Details(string id)
        {
            var viewModel = this.usersService.GetStudentById<CompanyViewModel>(id);
            return this.View(viewModel);
        }
    }
}
