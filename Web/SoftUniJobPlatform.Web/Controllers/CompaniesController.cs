namespace SoftUniJobPlatform.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Web.ViewModels.Categories;
    using SoftUniJobPlatform.Web.ViewModels.Companies;

    public class CompaniesController : Controller
    {
        private readonly ICompaniesService companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            this.companiesService = companiesService;
        }

        public IActionResult Index()
        {
            var viewModel = new CompaniesViewModel
            {
                Companies =
                    this.companiesService.GetAll<CompanyViewModel>(),
            };
            return this.View(viewModel);
        }
    }
}
