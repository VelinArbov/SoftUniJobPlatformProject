using System.Runtime.CompilerServices;

namespace SoftUniJobPlatform.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Identity;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }

        public string ImageUrl { get; set; }

        public string ViewImage => this.ImageUrl == null
            ? "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png"
            : this.ImageUrl;

        public DateTime CreatedOn { get; set; }

        public string ViewCreatedOn => this.CreatedOn.ToLongDateString();
    }
}