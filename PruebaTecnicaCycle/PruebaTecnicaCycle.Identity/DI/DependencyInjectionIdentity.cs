using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnicaCycle.Application.Interfaces;
using PruebaTecnicaCycle.Domain.Dtos.Identity;
using PruebaTecnicaCycle.Identity.Models;
using PruebaTecnicaCycle.Identity.Services;
using System.Text;

namespace PruebaTecnicaCycle.Identity.DI
{
    public static class DependencyInjectionIdentity
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration) 
        { 
            //instancio el appsettings para el Jwt
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            //instancion la conexion a BD
            services.AddDbContext<PruebaTecnicaCycleIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"),
                b => b.MigrationsAssembly(typeof(PruebaTecnicaCycleIdentityDbContext).Assembly.FullName));//lleve acabo la migracion
            });

            //Agrego la instancia para el aplication user

            services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<PruebaTecnicaCycleIdentityDbContext>()
                                                                .AddDefaultTokenProviders();

            //inyecto la interface de identity
            services.AddTransient<IAuthService, AuthService>();

            //inyecto las  propiedades para la  autenticacion
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer=true,
                    ValidateAudience=true,
                    ValidateLifetime=true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience= configuration["JwtSettings:Audience"],
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
            });

            return services;
        }
    }
}