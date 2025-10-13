using SIGA.Application.Core;
using SIGA.Application.Dtos.Identity.Account;
using SIGA.Application.Models;

namespace SIGA.Application.Contracts.identity
{
    public interface IProfileService 
    {
        Task<PerfilViewModel> GetUserByEmail(string userId);
        Task<ServiceResponse> UpdateProfile(PerfilDto perfilDto);

    }
}
