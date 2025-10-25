using SIGA.Application.Dtos.Identity.Account;
using SIGA.Application.Response;

namespace SIGA.Application.Contracts.Entities
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginDto loginDto);
        Task<RegisterResponse> RegisterAsync(RegisterDto registerDto, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task UploadPhotoAsync(RegisterDto registerDto);
        Task SignOutAsync();
    }
}
