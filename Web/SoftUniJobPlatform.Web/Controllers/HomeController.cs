using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SoftUniJobPlatform.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using SoftUniJobPlatform.Data;
    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Services.Mapping;
    using SoftUniJobPlatform.Web.ViewModels;


    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ICloudinaryService clodinaryService;

        public HomeController(ICategoriesService categoriesService,
            ICloudinaryService clodinaryService)
        {
            this.categoriesService = categoriesService;
            this.clodinaryService = clodinaryService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<string> UploadImg(IFormFile file)
        {
            ;
           return await this.clodinaryService.UploadFormFileAsync(file);
           
        }

     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
