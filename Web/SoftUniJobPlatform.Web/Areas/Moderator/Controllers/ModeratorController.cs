namespace SoftUniJobPlatform.Web.Areas.Moderator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Common;
    using SoftUniJobPlatform.Web.Controllers;

    [Authorize(Roles = GlobalConstants.ModeratorRoleName)]
    [Area("Moderator")]
    public class ModeratorController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

    }
}
