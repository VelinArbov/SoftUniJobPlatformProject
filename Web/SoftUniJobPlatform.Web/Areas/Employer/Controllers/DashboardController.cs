using System.Linq;
using System.Threading.Tasks;
using SoftUniJobPlatform.Web.Areas.Administration.Controllers;
using SoftUniJobPlatform.Web.Areas.Employer.Controllers;

namespace SoftUniJobPlatform.Web.Areas.Employer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Services.Data;


    public class DashboardController : EmployerController
    {

        public IActionResult Index()
        {
            return this.View();
        }

    }
}
