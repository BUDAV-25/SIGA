using AutoMapper;
using SIGA.Application.Contacts.Identity;
using SIGA.Application.Contracts.Entities;
using SIGA.Application.Dtos.Identity;
using SIGA.Application.Dtos.Identity.Account;
using SIGA.Application.Response;

namespace SIGA.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        public readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, string origin)
        {
            ForgotPasswordRequest forgotPasswordRequest = _mapper.Map<ForgotPasswordRequest>(forgotPasswordDto);
            return await _accountService.ForgotPasswordAsync(forgotPasswordRequest, origin);
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginDto loginDto)
        {
            AuthenticationRequest authenticationRequest = _mapper.Map<AuthenticationRequest>(loginDto);
            AuthenticationResponse response = await _accountService.AuthenticateAsync(authenticationRequest);
            return response;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterDto registerDto, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(registerDto);
            RegisterResponse response = await _accountService.RegisterUserAsync(registerRequest, origin);
            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            ResetPasswordRequest resetPassword = _mapper.Map<ResetPasswordRequest>(resetPasswordDto);
            return await _accountService.ResetPasswordAsync(resetPassword);
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task UploadPhotoAsync(RegisterDto registerDto)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(registerDto);
            await _accountService.UploadPhoto(registerRequest);
        }
    }
}
