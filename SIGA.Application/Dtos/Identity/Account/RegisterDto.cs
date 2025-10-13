using Microsoft.AspNetCore.Http;
using SIGA.Application.Dtos.Identity.Account.Base;
using System.ComponentModel.DataAnnotations;

namespace SIGA.Application.Dtos.Identity.Account
{
    public class RegisterDto : BaseAccountDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debe colocar su FirstName.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar su LastName.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar su FirstName de usuario.")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden.")]
        [Required(ErrorMessage = "Debe colocar una contraseña.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe colocar su correo.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar su telefono.")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }

        public string? Foto { get; set; }
        public IFormFile File { get; set; }

    }
}
