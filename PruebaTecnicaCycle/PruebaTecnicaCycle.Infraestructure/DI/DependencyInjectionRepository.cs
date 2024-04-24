using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnicaCycle.Domain.Repositories;
using PruebaTecnicaCycle.Infraestructure.Persistance;
using PruebaTecnicaCycle.Infraestructure.Repositories;

namespace PruebaTecnicaCycle.Infraestructure.DI
{
    public static class DependencyInjectionRepository
    {
        public static IServiceCollection AddInfraestructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDomainProductRepository,DomainProductRepository>();

            services.AddDbContext<PruebaTecnicaCycleDBContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("CycleBDAppContext"));
            });

            return services;
        }
    }
}