using System.Linq;
using System.Threading.Tasks;
using SoftUniJobPlatform.Web.Areas.Administration.Controllers;

namespace SoftUniJobPlatform.Web.Areas.Student.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Services.Data;

    public class DashboardController : StudentController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

    }
}
