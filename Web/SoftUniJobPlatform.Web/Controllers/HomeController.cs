using System;
using Microsoft.AspNetCore.Diagnostics;
using SoftUniJobPlatform.Web.ViewModels.HttpError;

namespace SoftUniJobPlatform.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Web.ViewModels;
    using SoftUniJobPlatform.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IApplicationUsersService usersService;
        private readonly IJobsService jobsService;
        private readonly ICloudinaryService clodinaryService;

        public HomeController(ICategoriesService categoriesService,
            IApplicationUsersService usersService,
            IJobsService jobsService,
            ICloudinaryService clodinaryService)
        {
            this.categoriesService = categoriesService;
            this.usersService = usersService;
            this.jobsService = jobsService;
            this.clodinaryService = clodinaryService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexCounterViewModel
            {
                Students = this.usersService.GetStudentsCount(),
                Companies = this.usersService.GetCompaniesCount(),
                Jobs = this.jobsService.GetCountJobs(),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult HttpError()
        {
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return this.View(new HttpErrorViewModel { Content = $"{exceptionHandlerPathFeature.Error.Message}"});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
