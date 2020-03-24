using SoftUniJobPlatform.Services.Data;
using SoftUniJobPlatform.Web.ViewModels.Categories;

namespace SoftUniJobPlatform.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult ByName(string name)
        {
            var viewModel =
                this.categoriesService.GetByName<CategoryViewModel>(name);
            return this.View(viewModel);
        }


    }
}
