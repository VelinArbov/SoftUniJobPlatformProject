﻿namespace SoftUniJobPlatform.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Data;
    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;
    using SoftUniJobPlatform.Web.ViewModels;

    public class HomeController : BaseController
    {
        private readonly IDeletableEntityRepository<Category> categories;

        public HomeController(IDeletableEntityRepository<Category> categories)
        {
            this.categories = categories;
        }



        public IActionResult Index()
        {
            return this.View();
        }


        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}