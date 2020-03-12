namespace SoftUniJobPlatform.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class IndexViewModel : IMapFrom<Category>
    {

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
