using System.ComponentModel.DataAnnotations;
using SoftUniJobPlatform.Data.Models;
using SoftUniJobPlatform.Services.Mapping;

namespace SoftUniJobPlatform.Web.ViewModels.Companies
{
    public class CompanyViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}