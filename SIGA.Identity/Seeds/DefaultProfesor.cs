using Microsoft.AspNetCore.Identity;
using SIGA.Identity.Enum;
using SIGA.Identity.Shared.Entities;

namespace SIGA.Identity.Seeds
{
    public class DefaultProfesor
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser applicationUser = new();

            applicationUser.UserName = "Maria";
            applicationUser.Email = "maria@siga.com";
            applicationUser.FirstName = "Maria";
            applicationUser.LastName = "Gonzalez";
            applicationUser.Gender = 'F';
            applicationUser.State = "Activo";
            applicationUser.DateOfEntry = DateTime.Parse("2022-08-01");
            applicationUser.CreatedAt = DateTime.Now;
            applicationUser.LastLogin = DateTime.Now;
            applicationUser.EmailConfirmed = true;
            applicationUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != applicationUser.Id))
            {
                var user = await userManager.FindByEmailAsync(applicationUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(applicationUser, "maria123");
                    await userManager.AddToRoleAsync(applicationUser, RolesEnum.Profesor.ToString());
                }
            }
        }
    }
}
