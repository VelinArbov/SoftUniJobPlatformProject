namespace SoftUniJobPlatform.Web.Areas.Employer.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Common;
    using SoftUniJobPlatform.Web.Controllers;

    [Authorize(Roles = GlobalConstants.EmployerRoleName + "," + GlobalConstants.ModeratorRoleName + "," + GlobalConstants.AdministratorRoleName)]
    [Area("Employer")]
    public class EmployerController : BaseController
    {
    }
}
