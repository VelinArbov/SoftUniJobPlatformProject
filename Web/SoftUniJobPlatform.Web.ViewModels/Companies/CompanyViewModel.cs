namespace SoftUniJobPlatform.Web.ViewModels.Companies
{
    using System.ComponentModel.DataAnnotations;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class CompanyViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Location { get; set; }
    }
}