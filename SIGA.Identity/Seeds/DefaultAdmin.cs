using Microsoft.AspNetCore.Identity;
using SIGA.Identity.Enum;
using SIGA.Identity.Shared.Entities;

namespace SIGA.Identity.Seeds
{
    public class DefaultAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser applicationUser = new();

            applicationUser.UserName = "admin";
            applicationUser.Email = "admin@siga.com";
            applicationUser.FirstName = "Admin";
            applicationUser.LastName = "Siga";
            applicationUser.Gender = 'F';
            applicationUser.DateOfBirth = DateOnly.Parse("1995-01-01");
            applicationUser.Province = "Santo Domingo";
            applicationUser.Sector = "Centro";
            applicationUser.Address = "Calle Principal #123";
            applicationUser.State = "Activo";
            applicationUser.DateOfEntry = DateOnly.Parse("2020-01-01");
            applicationUser.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
            applicationUser.LastLogin = DateOnly.FromDateTime(DateTime.Now);
            applicationUser.EmailConfirmed = true;
            applicationUser.PhoneNumberConfirmed = true;

            if(userManager.Users.All(u => u.Id != applicationUser.Id))
            {
                var user = await userManager.FindByEmailAsync(applicationUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(applicationUser, "admin123");
                    await userManager.AddToRoleAsync(applicationUser, RolesEnum.Administrador.ToString());
                }
            }
        }
    }
}
