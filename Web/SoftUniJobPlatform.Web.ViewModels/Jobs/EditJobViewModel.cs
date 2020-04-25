namespace SoftUniJobPlatform.Web.ViewModels.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class EditJobViewModel : IMapFrom<Job>
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете локация на обявата")]
        public string Location { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете ниво.")]
        public string Level { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете описание на обявата")]
        [StringLength(200, ErrorMessage = "Описанието трябва да е поне {2} и не повече от {1} символа.", MinimumLength = 20)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете ангажимент")]
        public string Engagement { get; set; }

        [Required]
        [Range(0, 10000,
            ErrorMessage = "Заплатата трябва да бъде между  {1} and {2} BGN.")]
        public int Salary { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете категорията на обявата")]
        public int CategoryId { get; set; }
    }
}
