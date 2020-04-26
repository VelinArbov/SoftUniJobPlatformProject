namespace SoftUniJobPlatform.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using SoftUniJobPlatform.Common;
    using SoftUniJobPlatform.Data.Models;

    public class UserRolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();
            var user = await userManager.FindByNameAsync(GlobalConstants.AdminUserName);
            var role = await roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName);

            if (user != null && role != null)
            {
                var exists = dbContext.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);

                if (exists)
                {
                    return;
                }
                else
                {
                    user.IsAdmin = true;

                    dbContext.UserRoles.Add(new IdentityUserRole<string>
                    {
                        RoleId = role.Id,
                        UserId = user.Id,
                    });

                    await dbContext.SaveChangesAsync();
                }
            }

            return;
        }
    }
}
