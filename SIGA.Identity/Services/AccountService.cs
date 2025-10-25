using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SIGA.Application.Contacts.Identity;
using SIGA.Application.Dtos.Identity;
using SIGA.Application.Response;
using SIGA.Identity.Enum;
using SIGA.Identity.Shared.Entities;
using System.Text;

namespace SIGA.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new AuthenticationResponse();

            var usuario = await _userManager.FindByEmailAsync(request.Email);

            if (usuario == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas registradas con el correo {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(usuario.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales invalidas para {request.Email}";
                return response;
            }

            if (!usuario.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Se necesita la activación del correo: {request.Email} para iniciar sesión.";
                return response;
            }

            response.Id = usuario.Id;
            response.Email = usuario.Email;
            response.UserName = usuario.UserName;

            var listaRoles = await _userManager.GetRolesAsync(usuario).ConfigureAwait(false);

            response.Roles = listaRoles.ToList();
            response.IsVerified = usuario.EmailConfirmed;

            return response;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var usuario = await _userManager.FindByIdAsync(userId);

            if (usuario == null)
            {
                return $"Ninguna cuenta registrada con este usuario.";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var result = await _userManager.ConfirmEmailAsync(usuario, token);

            if (result.Succeeded)
            {
                return $"La cuenta con el correo {usuario.Email} ha sido confirmada.";
            }
            else
            {
                return $"Ha ocurrido un error registrando el correo {usuario.Email}.";
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            /*ForgotPasswordResponse response = new ForgotPasswordResponse();

            var usuario = await _userManager.FindByEmailAsync(request.Email);

            if (usuario == null)
            {
                response.HasError = true;
                response.Error = $"Ninguna cuenta registrada con el correo {request.Email}.";
                return response;
            }

            var verificationURL = await _emailHelper.ForgotPasswordURL(usuario, origin);

            await _emailService.SendEmailAsync(new Infraestructure.Entities.EmailRequest()
            {
                To = usuario.Email,
                Body = $"Por favor, reinicia tu cuenta visitando esta URL {verificationURL}",
                Subject = "Restablecimiento de contraseña"
            });

            return response;*/

            throw new NotImplementedException();
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var usuarioSameUser = await _userManager.FindByNameAsync(request.UserName);

            if (usuarioSameUser != null)
            {
                response.HasError = true;
                response.Error = $"El FirstName de usuario {request.UserName} ya existe, por favor elija otro.";
                return response;
            }

            var usuarioSameEmail = await _userManager.FindByEmailAsync(request.Email);

            if (usuarioSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"El correo {request.Email} ya existe.";
                return response;
            }

            var usuario = new ApplicationUser
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Email = request.Email,
                PasswordHash = request.Password,
                State = request.State,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(usuario, request.Password);

            switch (request.Role)
            {
                case "Estudiante":
                    await _userManager.AddToRoleAsync(usuario, RolesEnum.Estudiante.ToString());
                    break;
                case "Profesor":
                    await _userManager.AddToRoleAsync(usuario, RolesEnum.Profesor.ToString());
                    break;
                case "Administrador":
                    await _userManager.AddToRoleAsync(usuario, RolesEnum.Administrador.ToString());
                    break;
                default:
                    response.HasError = true;
                    response.Error = $"El rol {request.Role} no es válido.";
                    return response;
            }

            /*if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, RolesEnum.Estudiante.ToString());
                var verificacionURL = await _emailHelper.VerificationEmailURL(usuario, origin);
                await _emailService.SendEmailAsync(new Infraestructure.Entities.EmailRequest()
                {
                    To = usuario.Email,
                    Body = $"Por favor, confirme su cuenta ingresando a esta URL: {verificacionURL}",
                    Subject = "Registro de confirmacion"
                });
            }
            else
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error intentando registrar el usuario.";
                return response;
            }*/
            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new ResetPasswordResponse();

            var usuario = await _userManager.FindByEmailAsync(request.Email);

            if (usuario == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas registradas con el correo: {request.Email}";
                return response;
            }
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.ConfirmPassword))
            {
                response.HasError = true;
                response.Error = $"Por favor llenar todos los campos.";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            var result = await _userManager.ResetPasswordAsync(usuario, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error al restablecer la contraseña";
                return response;
            }
            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task UploadPhoto(RegisterRequest registerDto)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(registerDto.Email);

            if (user != null)
            {
                user.Foto = registerDto.Foto;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                }
                else
                {
                }
            }
            else
            {
            }
        }
    }
}
