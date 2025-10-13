using SIGA.Application.Dtos.Identity.Account.Base;
using System.ComponentModel.DataAnnotations;

namespace SIGA.Application.Dtos.Identity.Account
{
    public class ForgotPasswordDto : BaseAccountDto
    {
        [Required(ErrorMessage = "Debe colocar su correo.")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

    }
}
