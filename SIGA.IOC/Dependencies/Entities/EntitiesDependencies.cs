using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using SIGA.Application.Contracts.Entities;
using SIGA.Application.Mapping.Identity;
using SIGA.Application.Response;
using SIGA.Application.Services;

namespace SIGA.IOC.Dependencies.Entities
{
    public static class EntitiesDependencies
    {
        public static void AllEnityDependencies(this IServiceCollection services)
        {
            #region Repositories

            #endregion

            #region Services
            services.AddTransient<AuthenticationResponse>();
            services.AddTransient<IUserService, UserService>();
            #endregion

            #region AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddAutoMapper(typeof(AllDboMapping));
            services.AddAutoMapper(typeof(IdentityMapping));
            #endregion

            #region Validators
            #endregion
        }
    }
}
