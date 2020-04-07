using Microsoft.AspNetCore.Identity;

namespace SoftUniJobPlatform.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }

    }
}
