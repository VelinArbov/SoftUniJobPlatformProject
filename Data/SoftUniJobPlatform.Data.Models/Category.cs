namespace SoftUniJobPlatform.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoftUniJobPlatform.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }


    }
}
