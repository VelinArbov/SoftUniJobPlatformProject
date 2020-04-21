using System;

namespace SoftUniJobPlatform.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Web.ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private const int ItemsPerPage = 5;
        private readonly ICategoriesService categoriesService;
        private readonly IJobsService jobsService;

        public CategoriesController(ICategoriesService categoriesService,
            IJobsService jobsService)
        {
            this.categoriesService = categoriesService;
            this.jobsService = jobsService;
        }

        public IActionResult Index()
        {
            var viewModel =
                this.categoriesService.GetAll<CategoryViewModel>();
            return this.View(viewModel);
        }

        public IActionResult ByName(string name, int page = 1)
        {
            var viewModel =
                this.categoriesService.GetByName<CategoryViewModel>(name);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.Jobs = this.jobsService.GetByCategoryId<JobsInCategoryViewModel>(viewModel.Id, ItemsPerPage, (page - 1) * ItemsPerPage);

            var count = this.jobsService.GetCountByCategoryId(viewModel.Id);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            return this.View();
        }

    }
}
