using Microsoft.Extensions.DependencyInjection;
using PruebaTecnicaCycle.Application.Interfaces;
using PruebaTecnicaCycle.Application.Services;

namespace PruebaTecnicaCycle.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAppProductService, AppProductService>();

            return services;
        }
    }
}