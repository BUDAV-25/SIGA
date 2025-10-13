

using Microsoft.AspNetCore.Http;

namespace SIGA.Application.Dtos.Identity.Account
{
    public class PerfilDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string ConfirmarContraseña { get; set; }
        public string Foto { get; set; }
        public IFormFile File { get; set; }
    }
}
