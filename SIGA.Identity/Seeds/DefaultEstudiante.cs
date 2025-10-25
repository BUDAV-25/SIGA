using Microsoft.AspNetCore.Identity;
using SIGA.Identity.Enum;
using SIGA.Identity.Shared.Entities;

namespace SIGA.Identity.Seeds
{
    public class DefaultEstudiante
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser applicationUser = new();

            applicationUser.UserName = "Juan";
            applicationUser.Email = "juanjose@siga.com";
            applicationUser.FirstName = "Juan Jose";
            applicationUser.LastName = "Perez";
            applicationUser.Gender = 'M';
            applicationUser.State = "Activo";
            applicationUser.DateOfEntry = DateTime.Parse("2013-01-01");
            applicationUser.CreatedAt = DateTime.Now;
            applicationUser.LastLogin = DateTime.Now;
            applicationUser.EmailConfirmed = true;
            applicationUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != applicationUser.Id))
            {
                var user = await userManager.FindByEmailAsync(applicationUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(applicationUser, "juan123");
                    await userManager.AddToRoleAsync(applicationUser, RolesEnum.Estudiante.ToString());
                }
            }
        }
    }
}
