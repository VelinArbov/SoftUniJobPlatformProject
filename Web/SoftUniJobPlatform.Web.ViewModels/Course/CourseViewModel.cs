namespace SoftUniJobPlatform.Web.ViewModels.Course
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Data.Models.Enum;
    using SoftUniJobPlatform.Services.Mapping;

    public class CourseViewModel : IMapFrom<Data.Models.Course>
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете заглавие на курса")]
        [StringLength(100, ErrorMessage = "Заглавието трябва да е поне {2} и не повече от {1} символа.", MinimumLength = 5)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете описание на обявата")]
        [StringLength(200, ErrorMessage = "Описанието трябва да е поне {2} и не повече от {1} символа.", MinimumLength = 20)]
        public string Description { get; set; }

        public string ShortDescription => this.Description == null ? "No Description" :
            this.Description.Length > 200 ? this.Description.Substring(0, 150) + "..." : this.Description;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете категорията ")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете снимка ")]
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
