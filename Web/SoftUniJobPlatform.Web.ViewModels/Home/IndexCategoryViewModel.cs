﻿using System.Net;
using System.Text.RegularExpressions;

namespace SoftUniJobPlatform.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class IndexCategoryViewModel : IMapFrom<Category>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Name { get; set; }

       // public string Url => $"/{this.Name.Replace(" ", "-")}";
    }
}
