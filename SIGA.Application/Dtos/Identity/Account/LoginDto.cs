using SIGA.Application.Dtos.Identity.Account.Base;
using System.ComponentModel.DataAnnotations;

namespace SIGA.Application.Dtos.Identity.Account
{
    public class LoginDto : BaseAccountDto
    {
        [Required(ErrorMessage = "Debe colocar el correo del usuario.")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
