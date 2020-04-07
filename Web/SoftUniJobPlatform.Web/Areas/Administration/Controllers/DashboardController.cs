namespace SoftUniJobPlatform.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Web.ViewModels.Administration.Dashboard;
    using SoftUniJobPlatform.Web.ViewModels.Categories;
    using SoftUniJobPlatform.Web.ViewModels.Jobs;

    public class DashboardController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IJobsService jobsService;
        private readonly IApplicationUsersService usersService;

        public DashboardController(
            ICategoriesService categoriesService,
            IJobsService jobsService,
            IApplicationUsersService usersService)
        {
            this.categoriesService = categoriesService;
            this.jobsService = jobsService;
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            var viewModel = new AllCategoriesViewModel
            {
                Categories = this.categoriesService.GetAll<CategoryViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Users(AllUsersViewModel model)
        {
            var viewModel = new AllUsersViewModel
            {
                Users = this.usersService.GetAll<UserViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult CreateCategory()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryViewModel model)
        {
            this.categoriesService.Create(model.Title, model.Description, model.ImageUrl);
            return this.View();
        }

        [HttpGet]
        public IActionResult DeleteCategory()
        {
            var viewModel = new AllCategoriesViewModel
            {
                Categories = this.categoriesService.GetAll<CategoryViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            this.categoriesService.Delete(id);
            return this.Ok("Delete");
        }

        public IActionResult CreateJob()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateJob(CategoryViewModel model)
        {
            return this.View();
        }

        public IActionResult RoleManagement()
        {
            var viewModel = this.usersService.GetAll<RolesViewModel>();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult RoleManagement(RolesViewModel model)
        {
            return this.View();
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
