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
                LastName = "Admin",
                DateOfBirth = new DateTime(1980, 1, 1),
                Oib = "00000000000",
                IdCardNumber = "000000000"
            };

            await userManager.CreateAsync(admin, "Admin123.?");

            await userManager.AddToRoleAsync(admin, ApplicationRole.Administrator.ToString());

            var instructor = new ApplicationUser
            {
                UserName = "instructor@instructor.com",
                Email = "instructor@instructor.com",
                EmailConfirmed = true,
                FirstName = "Instructor",
                LastName = "Instructor",
                DateOfBirth = new DateTime(1990, 1, 1),
                Oib = "11111111111",
                IdCardNumber = "111111111"
            };

            await userManager.CreateAsync(instructor, "Instructor123.?");

            await userManager.AddToRoleAsync(instructor, ApplicationRole.Instructor.ToString());

            var student = new ApplicationUser
            {
                UserName = "student@student.com",
                Email = "student@student.com",
                EmailConfirmed = true,
                FirstName = "Student",
                LastName = "Student",
                DateOfBirth = new DateTime(2000, 1, 1),
                Oib = "22222222222",
                IdCardNumber = "222222222"
            };

            await userManager.CreateAsync(student, "Student123.?");

            await userManager.AddToRoleAsync(student, ApplicationRole.Student.ToString());
        }

        await context.SaveChangesAsync();
    }
}
