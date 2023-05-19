using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace DSMS.DataAccess.Persistence;

public static class DatabaseContextSeed
{
    public static async Task SeedDatabaseAsync(DatabaseContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        if (!roleManager.Roles.Any())
        {
            foreach (var name in Enum.GetNames(typeof(ApplicationRole)))
            {
                await roleManager.CreateAsync(new IdentityRole(name));
            }
        }

        if (!userManager.Users.Any())
        {
            var user = new ApplicationUser { 
                UserName = "admin@admin.com", 
                Email = "admin@admin.com", 
                EmailConfirmed = true, 
                FirstName = "Admin", 
                LastName = "Admin" 
            };

            await userManager.CreateAsync(user, "Admin123.?");

            await userManager.AddToRoleAsync(user, "Administrator");
        }

        await context.SaveChangesAsync();
    }
}
