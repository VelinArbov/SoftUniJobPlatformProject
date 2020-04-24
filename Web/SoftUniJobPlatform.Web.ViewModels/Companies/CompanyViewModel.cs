using System.Collections.Generic;

namespace SoftUniJobPlatform.Web.ViewModels.Companies
{
    using System.ComponentModel.DataAnnotations;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class CompanyViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string ViewEmail => this.Email == null ? "No Email Address" : this.Email;

        public string PhoneNumber { get; set; }

        public string ViewPhoneNumber => this.PhoneNumber == null ? "No phone number" : this.PhoneNumber;
        
        [Required]
        public string FullName { get; set; }

        public string ViewImage => this.ImageUrl == null
            ? "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png"
            : this.ImageUrl;

        [Required]
        public string ImageUrl { get; set; }

        public string Location { get; set; }

        public ICollection<UsersSkill> UsersSkills { get; set; }
    }
}