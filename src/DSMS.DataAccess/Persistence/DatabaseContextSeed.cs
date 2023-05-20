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
            var admin = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Admin"
            };

            await userManager.CreateAsync(admin, "Admin123.?");

            await userManager.AddToRoleAsync(admin, "Administrator");

            var instructor = new ApplicationUser
            {
                UserName = "instructor@instructor.com",
                Email = "instructor@instructor.com",
                EmailConfirmed = true,
                FirstName = "Instructor",
                LastName = "Instructor"
            };

            await userManager.CreateAsync(instructor, "Instructor123.?");

            await userManager.AddToRoleAsync(instructor, "Instructor");

            var student = new ApplicationUser
            {
                UserName = "student@student.com",
                Email = "student@student.com",
                EmailConfirmed = true,
                FirstName = "Student",
                LastName = "Student"
            };

            await userManager.CreateAsync(student, "Student123.?");

            await userManager.AddToRoleAsync(student, "Student");
        }

        await context.SaveChangesAsync();
    }
}
