using Microsoft.AspNetCore.Identity;
using SIGA.Identity.Enum;
using SIGA.Identity.Shared.Entities;

namespace SIGA.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Administrador.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Estudiante.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Profesor.ToString()));
        }
    }
}
