namespace SoftUniJobPlatform.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Common;
    using SoftUniJobPlatform.Web.Controllers;

    [Authorize(Roles = GlobalConstants.StudentRoleName)]
    [Area("Student")]
    public class StudentController : BaseController
    {
    }
}
