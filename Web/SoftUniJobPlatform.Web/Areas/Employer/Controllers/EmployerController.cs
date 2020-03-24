namespace SoftUniJobPlatform.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Common;
    using SoftUniJobPlatform.Web.Controllers;

    [Authorize(Roles = GlobalConstants.EmployerRoleName)]
    [Area("Employer")]
    public class EmployerController : BaseController
    {

      
    }
}
