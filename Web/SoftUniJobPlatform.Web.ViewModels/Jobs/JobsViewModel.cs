namespace SoftUniJobPlatform.Web.ViewModels.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class JobsViewModel : IMapFrom<Job>
    {
        public int Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string CompanyLogo => this.ApplicationUser.ImageUrl == null
            ? "https://www.google.com/search?q=no+photo+available+image&sxsrf=ALeKk02n_dbDW0vh-4GdnmSngM44cbNpCA:1587682015414&tbm=isch&source=iu&ictx=1&fir=TY2seD6vZ8C5mM%253A%252C029W-ajBtZqZzM%252C_&vet=1&usg=AI4_-kQ62grbCeXZgpqFL1G2aRCHpbDCRw&sa=X&ved=2ahUKEwjU26200P_oAhViDGMBHSW7BOgQ9QEwAnoECAoQGA#imgrc=TY2seD6vZ8C5mM:"
            : this.ApplicationUser.ImageUrl;

        public string CompanyName => this.ApplicationUser.UserName;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете заглавие на обявата")]
        [StringLength(100, ErrorMessage = "Заглавието трябва да е поне {2} и не повече от {1} символа.", MinimumLength = 5)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете локация на обявата")]
        public string Location { get; set; }

        public DateTime CreatedOn { get; set; }

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

        public int PageCount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете категорията на обявата")]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public string ViewSalary => this.Salary == 0 ? "--" : this.Salary.ToString();

        public IEnumerable<StudentJob> Candidates { get; set; }
    }
}
