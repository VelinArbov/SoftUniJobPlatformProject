using SoftUniJobPlatform.Services.Data;
using SoftUniJobPlatform.Web.ViewModels.Jobs;

namespace SoftUniJobPlatform.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class JobsController : Controller
    {
        public IActionResult GetAll()
        {

            return this.View();
        }
    }
}
