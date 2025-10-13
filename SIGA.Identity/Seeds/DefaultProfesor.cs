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
            applicationUser.DateOfBirth = DateOnly.Parse("1990-05-15");
            applicationUser.Province = "Santo Domingo";
            applicationUser.Sector = "Gazcue";
            applicationUser.Address = "Calle 2 #2";
            applicationUser.State = "Activo";
            applicationUser.DateOfEntry = DateOnly.Parse("2022-08-01");
            applicationUser.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
            applicationUser.LastLogin = DateOnly.FromDateTime(DateTime.Now);
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
