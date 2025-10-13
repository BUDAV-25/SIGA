using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SIGA.Application.Contracts.identity;
using SIGA.Application.Core;
using SIGA.Application.Dtos.Identity.Account;
using SIGA.Application.Models;
using SIGA.Identity.Shared.Context;
using SIGA.Identity.Shared.Entities;

namespace SIGA.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IdentityContext _identityContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileService(IdentityContext identityContext, UserManager<ApplicationUser> userManager)
        {
            _identityContext = identityContext;
            _userManager = userManager;
        }

        public async Task<PerfilViewModel?> GetUserByEmail(string userId)
        {
            try
            {
                var user = await _identityContext.Users
                    .Where(dbo => dbo.Id == userId)
                    .Select(dbo => new PerfilViewModel
                    {
                        Id = dbo.Id,
                        FirstName = dbo.FirstName,
                        LastName = dbo.LastName,
                        Telefono = dbo.PhoneNumber,
                        Email = dbo.Email,
                        Foto = dbo.Foto
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el perfil del usuario.", ex);
            }
        }

        public async Task<ServiceResponse> UpdateProfile(PerfilDto perfilDto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var user = await _userManager.FindByIdAsync(perfilDto.Id);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Messages = "Usuario no encontrado.";
                    return response;
                }

                bool isPasswordValid = await _userManager.CheckPasswordAsync(user, perfilDto.Contraseña);
                if (!isPasswordValid)
                {
                    response.IsSuccess = false;
                    response.Messages = "La contraseña es incorrecta.";
                    return response;
                }

                user.FirstName = perfilDto.FirstName;
                user.LastName = perfilDto.LastName;
                user.PhoneNumber = perfilDto.Telefono;
                user.Email = perfilDto.Email;
                user.Foto = perfilDto.Foto;

                var result = await _userManager.UpdateAsync(user);

                perfilDto.Foto = user.Foto;
                response.Model = perfilDto; 

                if (!result.Succeeded)
                {
                    response.IsSuccess = false;
                    response.Messages = "Error al actualizar el usuario.";
                }
                
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ocurrió un error inesperado.";
            }

            return response;
        }

    }
}
