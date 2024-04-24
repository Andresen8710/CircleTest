using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaCycle.Identity.Configuration;
using PruebaTecnicaCycle.Identity.Models;

namespace PruebaTecnicaCycle.Identity
{
    public class PruebaTecnicaCycleIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public PruebaTecnicaCycleIdentityDbContext(DbContextOptions<PruebaTecnicaCycleIdentityDbContext> options) : base(options) //hago esto para que la cadena de conexion se inyecte en el contructor
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}