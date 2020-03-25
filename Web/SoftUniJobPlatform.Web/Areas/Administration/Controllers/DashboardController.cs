using System.Linq;
using System.Threading.Tasks;
using SoftUniJobPlatform.Web.ViewModels.Categories;

namespace SoftUniJobPlatform.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;

        public DashboardController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult CreateCategory()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryViewModel model)
        {
            this.categoriesService.CreateCategory(model.Title, model.Description, model.ImageUrl);
            return this.View();
        }

        public IActionResult RoleManagement()
        {
            return this.Redirect("/Home");
        }

        public IActionResult AddCompany()
        {
            return this.Redirect("/Home");
        }


        public IActionResult DeleteCompany()
        {
            return this.Redirect("/Home");
        }

    }
}
