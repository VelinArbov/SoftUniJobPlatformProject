﻿
namespace SoftUniJobPlatform.Web.ViewModels.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Text.RegularExpressions;

    using Microsoft.AspNetCore.Http;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        //[Required]
        public string ImageUrl { get; set; }

        public IFormFile Image { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

       // public string Url => $"/{this.Title.Replace(' ', '-')}";

        public IEnumerable<JobsInCategoryViewModel> Jobs { get; set; }
    }
}
