using Microsoft.EntityFrameworkCore;
using SIGA.Identity.Register;
using SIGA.IOC.Dependencies.Entities;
using SIGA.Persistance.Context;
using SIGA.Web.Helpers.Base;
using SIGA.Web.Middlewares;
using SocialNetwork.Web.Helpers.Perfil;

namespace SIGA.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Context
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SIGADB")));

            //Inyeccion de dependencias
            builder.Services.AllEnityDependencies();

            //Dependencias de Identity
            builder.Services.AddIdentityLayer(builder.Configuration);

            builder.Services.AddIdentityService();

            builder.Services.AddScoped<LoginAuthorize>();
            builder.Services.AddScoped<ValidateUserSesion>();
            builder.Services.AddScoped<PerfilHelper>();
            builder.Services.AddScoped<LoadPhoto>();

            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope())
            {
                await scope.ServiceProvider.RunIdentitySeeds();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Users}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}
