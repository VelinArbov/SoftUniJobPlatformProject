using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUniJobPlatform.Web.ViewModels.Course
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Data.Models.Enum;
    using SoftUniJobPlatform.Services.Mapping;

    public class CourseViewModel : IMapFrom<Data.Models.Course>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description == null ? "No Description" :
            this.Description.Length > 200 ? this.Description.Substring(0, 150) + "..." : this.Description;

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string CourseProgress { get; set; }

        public double Rate { get; set; }

        public int Credit { get; set; }

        [NotMapped]
        public string ApplicationUserId { get; set; }
    }
}
