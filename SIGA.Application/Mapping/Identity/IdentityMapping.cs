using AutoMapper;
using SIGA.Application.Dtos.Identity;
using SIGA.Application.Dtos.Identity.Account;
using SIGA.Application.Models;

namespace SIGA.Application.Mapping.Identity
{
    public class IdentityMapping : Profile
    {
        public IdentityMapping()
        {
            CreateMap<AuthenticationRequest, LoginDto>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, RegisterDto>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordDto>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordDto>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<PerfilDto, PerfilViewModel>()
                .ReverseMap();

            CreateMap<RegisterRequest, RegisterDto>()
                .ReverseMap();
        }
    }
}
