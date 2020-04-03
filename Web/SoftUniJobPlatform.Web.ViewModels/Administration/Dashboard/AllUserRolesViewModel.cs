namespace SoftUniJobPlatform.Web.ViewModels.Administration.Dashboard
{
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Services.Mapping;

    public class AllUserRolesViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }

    }
}
