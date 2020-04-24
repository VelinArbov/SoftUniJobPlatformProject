
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SoftUniJobPlatform.Data.Models;

namespace SoftUniJobPlatform.Web.ViewModels.Jobs
{
    public class JobsInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете заглавие на обявата")]
        [StringLength(100, ErrorMessage = "Заглавието трябва да е поне {2} и не повече от {1} символа.", MinimumLength = 5)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете локация на обявата")]
        public string Location { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете описание на обявата")]
        [StringLength(200, ErrorMessage = "Описанието трябва да е поне {2} и не повече от {1} символа.", MinimumLength = 20)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете ангажимент")]
        public string Engagement { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете ниво.")]
        public string Level { get; set; }

        [Required]
        [Range(0, 10000,
            ErrorMessage = "Заплатата трябва да бъде между  {1} and {2} BGN.")]
        public int Salary { get; set; }

     
        public string Position { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете категорията ")]
        public int CategoryId { get; set; }

    }
}
