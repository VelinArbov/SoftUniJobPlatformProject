
using System.Collections.Generic;
using SoftUniJobPlatform.Data.Models;
using SoftUniJobPlatform.Services.Mapping;

namespace SoftUniJobPlatform.Web.ViewModels.Categories
{
    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<JobsInCategoryViewModel> Jobs { get; set; }
    }
}
