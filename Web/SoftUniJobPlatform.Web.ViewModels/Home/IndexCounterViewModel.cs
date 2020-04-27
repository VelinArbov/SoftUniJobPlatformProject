using SoftUniJobPlatform.Web.ViewModels.Categories;

namespace SoftUniJobPlatform.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;
    using SoftUniJobPlatform.Web.ViewModels.Jobs;

    public class IndexCounterViewModel : IMapTo<Category>
    {
        public IEnumerable<JobsViewModel> JobsImages { get; set; }

        public int Students { get; set; }

        public int JobsCount { get; set; }

        public int Companies { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }


    }
}
