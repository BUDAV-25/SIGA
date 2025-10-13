using SIGA.Application.Dtos.Identity;
using SIGA.Application.Response;

namespace SIGA.Application.Contacts.Identity
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest);
        Task SignOutAsync();
        Task<RegisterResponse> RegisterEstudentUserAsync(RegisterRequest request, string origin);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task UploadPhoto(RegisterRequest registerDto);
    }
}
